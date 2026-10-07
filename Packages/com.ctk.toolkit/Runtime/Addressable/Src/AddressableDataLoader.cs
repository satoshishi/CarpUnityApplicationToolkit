using System.Collections.Generic;
using System.Threading;
using CTK.DataEvent;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace CTK.Addressable
{
    /// <summary>
    /// 指定されたAddressableグループ名からT型のアセット群を非同期でロードするデータローダー
    /// </summary>
    /// <typeparam name="T">ロードするアセットの型</typeparam>
    public class AddressableDataLoader<T> : IDataLoader<string, T[]>
    {
        /// <summary>
        /// 指定されたグループ名（ラベル）に対応するT型のアセット群をAddressablesからロードして返す
        /// </summary>
        /// <param name="groupName">ロード対象のAddressableグループ名（ラベル）</param>
        /// <param name="token">キャンセレーショントークン</param>
        /// <returns>ロードされたアセットの配列</returns>
        public async UniTask<T[]> LoadAsync(string groupName, CancellationToken token)
        {
            AsyncOperationHandle<IList<T>> handle =
                Addressables.LoadAssetsAsync<T>(groupName, null);

            IList<T> result = await handle.ToUniTask(cancellationToken: token);

            T[] array = new T[result.Count];
            result.CopyTo(array, 0);
            return array;
        }
    }
}
