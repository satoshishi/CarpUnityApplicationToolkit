using System;
using Cysharp.Threading.Tasks;

namespace CTK.State
{
    /// <summary>
    /// 状態を表すインターフェース。各状態はこのインターフェースを実装する
    /// </summary>
    public interface IState : IDisposable
    {
        /// <summary>
        /// 一番最初のStateかどうか
        /// </summary>
        bool EntryPoint { get; }

        /// <summary>
        /// この状態に遷移した際にコールされる
        /// </summary>
        UniTask OnEnter();

        /// <summary>
        /// この状態が終了した際にコールされる
        /// </summary>
        UniTask OnExit();

        /// <summary>
        /// この状態が中断された際にコールされる
        /// </summary>
        UniTask OnSuspend();

        /// <summary>
        /// この状態が再開された際にコールされる
        /// </summary>
        UniTask OnResume();
    }
}
