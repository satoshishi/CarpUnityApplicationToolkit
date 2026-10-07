using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CTK.UI
{
    /// <summary>
    /// UIBehaviourの生成・参照・破棄を管理するコンテナ。UIContainerConfigから構築される
    /// </summary>
    public class UIContainer
    {
        /// <summary>
        /// このContainerの設定ファイル
        /// </summary>
        private readonly UIContainerConfig config;

        /// <summary>
        /// VContainerによる依存解決を担うUIBehaviourファクトリ。prefabを渡すとDI済みインスタンスを返す
        /// </summary>
        private readonly Func<UIBehaviour, UIBehaviour> factory;

        /// <summary>
        /// UIのルートとなるCanvasコンポーネント。RootCanvasPrefabから生成される
        /// </summary>
        private Canvas rootCanvas;

        /// <summary>
        /// 生成済みUIBehaviourインスタンスのキャッシュ。Typeをキーとして管理する
        /// </summary>
        private readonly Dictionary<Type, UIBehaviour> instances = new Dictionary<Type, UIBehaviour>();

        /// <summary>
        /// このContainerを識別するキー
        /// </summary>
        public string Key => config.ContainerKey;

        /// <summary>
        /// コンストラクタ。rootCanvasPrefabからCanvasを生成し初期化する
        /// </summary>
        /// <param name="config">Containerの設定ファイル</param>
        /// <param name="factory">VContainerが提供するUIBehaviourファクトリ</param>
        /// <exception cref="ArgumentNullException">configまたはfactoryがnullの場合</exception>
        /// <exception cref="InvalidOperationException">RootCanvasPrefabにCanvasコンポーネントが存在しない場合</exception>
        public UIContainer(UIContainerConfig config, Func<UIBehaviour, UIBehaviour> factory)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));

            GameObject rootCanvasObject = UnityEngine.Object.Instantiate(config.RootCanvasPrefab);
            rootCanvas = rootCanvasObject.GetComponent<Canvas>();

            if (rootCanvas == null)
            {
                throw new InvalidOperationException(
                    $"RootCanvasPrefab '{config.RootCanvasPrefab.name}' does not have a Canvas component.");
            }
        }

        /// <summary>
        /// 指定した型のUIBehaviourを取得する。未生成の場合はfactoryで生成してActivateを呼び出す
        /// </summary>
        /// <typeparam name="T">取得または生成するUIBehaviourの型</typeparam>
        /// <returns>対応するUIBehaviourのインスタンス</returns>
        /// <exception cref="InvalidOperationException">対応するPrefabが設定ファイルに存在しない場合</exception>
        public async UniTask<T> GetOrCreate<T>() where T : UIBehaviour
        {
            Type type = typeof(T);

            if (instances.TryGetValue(type, out UIBehaviour existing))
            {
                return (T)existing;
            }

            UIBehaviour prefab = FindPrefab<T>();

            if (prefab == null)
            {
                throw new InvalidOperationException(
                    $"Prefab for type '{type.Name}' is not registered in UIContainerConfig '{config.ContainerKey}'.");
            }

            UIBehaviour instance = factory(prefab);
            instance.transform.SetParent(rootCanvas.transform, false);
            instances[type] = instance;

            await instance.Activate();

            return (T)instance;
        }

        /// <summary>
        /// 指定した型のUIBehaviourのInactivateを呼び出してから破棄する
        /// </summary>
        /// <typeparam name="T">破棄するUIBehaviourの型</typeparam>
        public async UniTask Destroy<T>() where T : UIBehaviour
        {
            Type type = typeof(T);

            if (!instances.TryGetValue(type, out UIBehaviour instance))
            {
                return;
            }

            instances.Remove(type);

            await instance.Inactivate();
            UnityEngine.Object.Destroy(instance.gameObject);
        }

        /// <summary>
        /// 全てのUIBehaviourのInactivateを並行して呼び出してからインスタンスとrootCanvasを破棄する
        /// </summary>
        public async UniTask DestroyAll()
        {
            UIBehaviour[] targets = new UIBehaviour[instances.Count];
            instances.Values.CopyTo(targets, 0);
            instances.Clear();

            UniTask[] inactivateTasks = new UniTask[targets.Length];

            for (int i = 0; i < targets.Length; i++)
            {
                inactivateTasks[i] = targets[i] != null
                    ? targets[i].Inactivate()
                    : UniTask.CompletedTask;
            }

            await UniTask.WhenAll(inactivateTasks);

            foreach (UIBehaviour instance in targets)
            {
                if (instance != null)
                {
                    UnityEngine.Object.Destroy(instance.gameObject);
                }
            }

            if (rootCanvas != null)
            {
                UnityEngine.Object.Destroy(rootCanvas.gameObject);
                rootCanvas = null;
            }
        }

        /// <summary>
        /// 設定ファイルのPrefab群から指定した型に一致するPrefabを返す
        /// </summary>
        /// <typeparam name="T">検索するUIBehaviourの型</typeparam>
        /// <returns>一致するPrefab。存在しない場合はnull</returns>
        private UIBehaviour FindPrefab<T>() where T : UIBehaviour
        {
            foreach (UIBehaviour prefab in config.UIBehaviourPrefabs)
            {
                if (prefab is T)
                {
                    return prefab;
                }
            }

            return null;
        }
    }
}
