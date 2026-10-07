using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CTK.UI
{
    /// <summary>
    /// UIの基底クラス。全てのUIはこのクラスを継承して実装する
    /// </summary>
    public abstract class UIBehaviour : MonoBehaviour
    {
        /// <summary>
        /// UIをアクティブ状態にする
        /// </summary>
        /// <returns>完了を通知するUniTask</returns>
        public abstract UniTask Activate();

        /// <summary>
        /// UIを非アクティブ状態にする
        /// </summary>
        /// <returns>完了を通知するUniTask</returns>
        public abstract UniTask Inactivate();
    }
}
