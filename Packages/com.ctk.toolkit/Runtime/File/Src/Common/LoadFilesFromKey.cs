using System.Threading;
using CTK.DataEvent;
using Cysharp.Threading.Tasks;

namespace CTK.File
{
    public class LoadFilesFromKey<T1, T2> : IDataLoader<T1, T2[]> where T1 : IFileLoadParameter
    {
        private readonly IDataLoader<string, T2[]> dataLoader;

        public LoadFilesFromKey(IDataLoader<string, T2[]> dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public async UniTask<T2[]> LoadAsync(T1 parameter, CancellationToken token)
        {
            T2[] files = await dataLoader.LoadAsync(parameter.Key, token);
            return files;
        }
    }
}
