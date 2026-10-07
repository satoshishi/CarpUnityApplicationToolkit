using System.Collections.Generic;
using UnityEngine;

namespace CTK.UI
{
    /// <summary>
    /// UIContainerの構築に必要な設定を保持するScriptableObject
    /// </summary>
    [CreateAssetMenu(fileName = "UIContainerConfig", menuName = "CTK/UI/UIContainerConfig")]
    public class UIContainerConfig : ScriptableObject
    {
        /// <summary>
        /// このContainerを識別するキー
        /// </summary>
        [SerializeField]
        private string containerKey;

        /// <summary>
        /// UIのルートとなるCanvasコンポーネントを持つPrefab
        /// </summary>
        [SerializeField]
        private GameObject rootCanvasPrefab;

        /// <summary>
        /// このContainerが管理するUIBehaviourのPrefab群
        /// </summary>
        [SerializeField]
        private List<UIBehaviour> uiBehaviourPrefabs;

        /// <summary>
        /// このContainerを識別するキー
        /// </summary>
        public string ContainerKey => containerKey;

        /// <summary>
        /// UIのルートとなるCanvasコンポーネントを持つPrefab
        /// </summary>
        public GameObject RootCanvasPrefab => rootCanvasPrefab;

        /// <summary>
        /// このContainerが管理するUIBehaviourのPrefab群
        /// </summary>
        public IReadOnlyList<UIBehaviour> UIBehaviourPrefabs => uiBehaviourPrefabs;
    }
}
