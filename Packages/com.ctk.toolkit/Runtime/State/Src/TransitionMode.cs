namespace CTK.State
{
    /// <summary>
    /// 状態遷移時の直前状態に対する処理モード
    /// </summary>
    public enum TransitionMode
    {
        /// <summary>
        /// 直前の状態をOnExitで終了し、スタックから除去する
        /// </summary>
        Exit,

        /// <summary>
        /// 直前の状態をOnSuspendで中断し、スタックに残す（RevertStateで再開可能）
        /// </summary>
        Suspend
    }
}
