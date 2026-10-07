using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CTK.State
{
    /// <summary>
    /// IStateのスタック管理と状態遷移を担うstaticクラス
    /// </summary>
    public static class StateExtensions
    {
        /// <summary>
        /// 状態履歴スタック。RevertStateによる巻き戻しに使用する
        /// </summary>
        private static readonly Stack<IState> stateStack = new Stack<IState>();

        /// <summary>
        /// 現在アクティブな状態。状態がない場合はnull
        /// </summary>
        public static IState CurrentState => stateStack.Count > 0 ? stateStack.Peek() : null;

        /// <summary>
        /// 指定したIStateインスタンスへ状態遷移する
        /// </summary>
        /// <param name="nextState">遷移先の状態インスタンス</param>
        /// <param name="mode">直前状態に対する処理モード</param>
        /// <exception cref="ArgumentNullException">nextStateがnullの場合</exception>
        public static async UniTask ChangeStateAsync(this IState nextState, TransitionMode mode = TransitionMode.Exit)
        {
            if (nextState == null) throw new ArgumentNullException(nameof(nextState));

            await ExitCurrentStateAsync(mode);

            stateStack.Push(nextState);

            Debug.Log($"Enter {nextState.GetType()}");
            await nextState.OnEnter();
        }

        /// <summary>
        /// 現在の状態をOnExitで終了し、一つ前の状態をOnResumeで再開する
        /// </summary>
        /// <exception cref="InvalidOperationException">巻き戻せる状態履歴がない場合</exception>
        public static async UniTask RevertStateAsync(this IState currentState)
        {
            if (stateStack.Count < 2)
            {
                throw new InvalidOperationException("RevertState failed: no previous state to revert to.");
            }

            IState current = stateStack.Pop();
            Debug.Log($"Exit {current.GetType()}");
            await current.OnExit();

            current.Dispose();

            Debug.Log($"Resume {stateStack.Peek().GetType()}");
            await stateStack.Peek().OnResume();
        }

        /// <summary>
        /// TransitionModeに応じて現在の状態を終了または中断する
        /// </summary>
        /// <param name="mode">処理モード</param>
        private static async UniTask ExitCurrentStateAsync(TransitionMode mode)
        {
            if (stateStack.Count == 0)
            {
                return;
            }

            if (mode == TransitionMode.Exit)
            {
                IState current = stateStack.Pop();
                Debug.Log($"Exit {current.GetType()}");
                await current.OnExit();
                current.Dispose();
            }
            else
            {
                Debug.Log($"Suspend {stateStack.Peek().GetType()}");
                await stateStack.Peek().OnSuspend();
            }
        }
    }
}
