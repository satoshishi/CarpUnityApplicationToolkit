using System;
using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace CTK.State
{
    /// <summary>
    /// IStateを継承し、合成基点で登録されたState群をマップ管理する基底クラス。
    /// サブクラスはGetState&lt;T&gt;()で遷移先Stateを取得できる
    /// </summary>
    public abstract class MappedState : IState, IInitializable
    {
        /// <summary>
        /// 型からStateインスタンスを解決するファクトリー。コンテナ構築後に実行することで循環依存を回避する
        /// </summary>
        private readonly Func<Type, MappedState> stateResolver;

        /// <inheritdoc/>
        public virtual bool EntryPoint { get; set; } = false;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="stateResolver">型からStateインスタンスを解決するファクトリー</param>
        protected MappedState(Func<Type, MappedState> stateResolver)
        {
            this.stateResolver = stateResolver;
        }

        /// <summary>
        /// 登録されたState群から型で遷移先Stateを取得する
        /// </summary>
        /// <typeparam name="T">取得するStateの型</typeparam>
        /// <returns>対応するStateインスタンス</returns>
        protected T GetState<T>() where T : MappedState
        {
            return (T)stateResolver(typeof(T));
        }

        /// <summary>
        /// コンテナ構築完了後に呼ばれる初期化処理。EntryPointがtrueなら状態遷移を開始する
        /// </summary>
        public virtual void Initialize()
        {
            if (EntryPoint)
            {
                this.ChangeStateAsync().Forget();
            }
        }

        /// <inheritdoc/>
        public abstract UniTask OnEnter();

        /// <inheritdoc/>
        public virtual UniTask OnExit() { return UniTask.CompletedTask; }

        /// <inheritdoc/>
        public virtual UniTask OnSuspend() { return UniTask.CompletedTask; }

        /// <inheritdoc/>
        public virtual UniTask OnResume() { return UniTask.CompletedTask; }

        /// <inheritdoc/>
        public virtual void Dispose() { }
    }
}
