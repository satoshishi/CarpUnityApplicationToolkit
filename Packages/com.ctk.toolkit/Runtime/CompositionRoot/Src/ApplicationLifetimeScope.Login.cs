using CTK.DataEvent;
using CTK.Login;
using VContainer;

namespace CTK.CompositionRoot
{
    public partial class ApplicationLifetimeScope
    {
        /// <summary>
        /// Login機能の登録。サブクラスでオーバーライドして差し替え可能
        /// </summary>
        /// <param name="builder">DIコンテナビルダー</param>
        protected virtual void ConfigureLogin(IContainerBuilder builder)
        {
            RegistrationLoginResultReactiveProperty(builder);

            RegisrationLoginUsecase(builder);
        }

        protected virtual void RegisrationLoginUsecase(IContainerBuilder builder)
        {
            builder.Register(
                resolver => new LoginDummyUser(
                    parameter => new SimpleLoginResult(true, "999")
                ),Lifetime.Singleton);

            builder.Register<IDataLoader<ILoginParameter, ILoginResult>>(
                resolver => new LoginUser(
                    resolver.Resolve<LoginDummyUser>(),
                    resolver.Resolve<IDataReactiveProperty<ILoginResult>>()
                ),
                Lifetime.Singleton
            );
        }

        protected virtual void RegistrationLoginResultReactiveProperty(IContainerBuilder builder)
        {
            ILoginResult loginResultStore = null;
            IDataReactiveProperty<ILoginResult> reactiveProperty = new VariableDataReactiveProperty<ILoginResult>(() => loginResultStore, value => loginResultStore = value);

            builder.RegisterInstance(reactiveProperty);
        }
    }
}
