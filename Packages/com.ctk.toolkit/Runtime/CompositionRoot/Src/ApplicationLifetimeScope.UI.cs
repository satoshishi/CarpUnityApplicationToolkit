using System;
using CTK.Addressable;
using CTK.DataEvent;
using CTK.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

namespace CTK.CompositionRoot
{
    public partial class ApplicationLifetimeScope
    {
        /// <summary>
        /// UI機能の登録。サブクラスでオーバーライドして差し替え可能
        /// </summary>
        /// <param name="builder">DIコンテナビルダー</param>
        protected virtual void ConfigureUI(IContainerBuilder builder)
        {
            builder.RegisterFactory<UIBehaviour, UIBehaviour>(
                resolver => prefab =>
                {
                    UIBehaviour instance = Instantiate(prefab);
                    resolver.InjectGameObject(instance.gameObject);
                    return instance;
                },
                Lifetime.Singleton
            );

            builder.Register<AddressableDataLoader<UIContainerConfig>>(Lifetime.Singleton)
                .As<IDataLoader<string, UIContainerConfig[]>>();

            builder.Register<UIContainerCollection>(Lifetime.Singleton);
        }

        /// <summary>
        /// GameObjectからDataEventインターフェースへの変換ファクトリーの登録。サブクラスでオーバーライドして差し替え可能
        /// </summary>
        /// <param name="builder">DIコンテナビルダー</param>
        protected virtual void ConfigureUIDataEvent(IContainerBuilder builder)
        {
            builder.RegisterInstance<Func<GameObject, IDataReactiveProperty<string>>>(
                gameObject =>
                {
                    TMP_InputField inputField = gameObject.GetComponent<TMP_InputField>();
                    return new VariableDataReactiveProperty<string>(
                        inputField.onValueChanged,
                        () => inputField.text,
                        value => inputField.text = value
                    );
                }
            );

            builder.RegisterInstance<Func<GameObject, IDataReactiveProperty>>(
                gameObject =>
                {
                    Button button = gameObject.GetComponent<Button>();
                    return new NonVariableDataReactiveProperty(button.onClick);
                }
            );

            builder.RegisterInstance<Func<GameObject, IDataReactiveProperty<bool>>>(
                gameObject =>
                {
                    Toggle toggle = gameObject.GetComponent<Toggle>();
                    return new VariableDataReactiveProperty<bool>(
                        toggle.onValueChanged,
                        () => toggle.isOn,
                        value => toggle.isOn = value
                    );
                }
            );
        }
    }
}
