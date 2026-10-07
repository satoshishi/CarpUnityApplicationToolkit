using System;
using System.Collections.Generic;
using System.Threading;
using CTK.DataEvent;
using Cysharp.Threading.Tasks;

namespace CTK.UI
{
    /// <summary>
    /// 複数のUIContainerをDictionaryで集約し、stringキーによる参照を提供するクラス
    /// </summary>
    public class UIContainerCollection
    {
        /// <summary>
        /// UIContainerConfigをロードするAddressableグループ名
        /// </summary>
        private const string GroupName = "UIConfigGroup";

        /// <summary>
        /// UIContainerConfig群を非同期でロードするデータローダー
        /// </summary>
        private readonly IDataLoader<string, UIContainerConfig[]> dataLoader;

        /// <summary>
        /// VContainerによる依存解決を担うUIBehaviourファクトリ
        /// </summary>
        private readonly Func<UIBehaviour, UIBehaviour> factory;

        /// <summary>
        /// ContainerKeyをキーとしてUIContainerを管理するDictionary
        /// </summary>
        private Dictionary<string, UIContainer> containers;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dataLoader">UIContainerConfig群をロードするデータローダー</param>
        /// <param name="factory">VContainerが提供するUIBehaviourファクトリ</param>
        /// <exception cref="ArgumentNullException">dataLoaderまたはfactoryがnullの場合</exception>
        public UIContainerCollection(IDataLoader<string, UIContainerConfig[]> dataLoader, Func<UIBehaviour, UIBehaviour> factory)
        {
            this.dataLoader = dataLoader ?? throw new ArgumentNullException(nameof(dataLoader));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        /// <summary>
        /// dataLoaderからConfig群を取得し、UIContainerを構築する
        /// </summary>
        /// <param name="token">キャンセレーショントークン</param>
        /// <exception cref="ArgumentException">同一キーのUIContainerConfigが複数存在する場合</exception>
        public async UniTask LoadConfigAsync(CancellationToken token)
        {
            UIContainerConfig[] configs = await dataLoader.LoadAsync(GroupName, token);

            containers = new Dictionary<string, UIContainer>(configs.Length);

            foreach (UIContainerConfig config in configs)
            {
                if (containers.ContainsKey(config.ContainerKey))
                {
                    throw new ArgumentException(
                        $"Duplicate ContainerKey detected: '{config.ContainerKey}'.");
                }

                containers[config.ContainerKey] = new UIContainer(config, factory);
            }
        }

        /// <summary>
        /// 指定したキーに対応するUIContainerを返す
        /// </summary>
        /// <param name="key">取得するUIContainerのキー</param>
        /// <returns>対応するUIContainer</returns>
        /// <exception cref="KeyNotFoundException">指定したキーのUIContainerが存在しない場合</exception>
        public async UniTask<UIContainer> GetAsync(string key)
        {
            if (containers == null)
            {
                await LoadConfigAsync(default);
            }

            if (containers.TryGetValue(key, out UIContainer container))
            {
                return container;
            }

            throw new KeyNotFoundException($"UIContainer not found for key: '{key}'.");
        }
    }
}
