using VContainer;
using VContainer.Unity;

namespace CTK.CompositionRoot
{
    /// <summary>
    /// CTK の機能群を登録する合成基点。
    /// 機能ごとの登録は各 partial ファイルに分離されており、サブクラスで個別にオーバーライド可能
    /// </summary>
    public partial class ApplicationLifetimeScope : LifetimeScope
    {
        /// <inheritdoc/>
        protected override void Configure(IContainerBuilder builder)
        {
            ConfigureUI(builder);
            ConfigureUIDataEvent(builder);
            ConfigureLogin(builder);
        }
    }
}
