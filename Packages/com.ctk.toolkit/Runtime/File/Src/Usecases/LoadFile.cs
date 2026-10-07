using System.Threading;
using CTK.DataEvent;
using Cysharp.Threading.Tasks;

namespace CTK.File
{
    public class LoadFile<T1, T2> : IDataLoader<T1, T2> where T1 : IFileLoadParameter
    {
        private readonly IDataLoader<T1, T2> dataLoader;

        public LoadFile(IDataLoader<T1, T2> dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public async UniTask<T2> LoadAsync(T1 parameter, CancellationToken token)
        {
            return await dataLoader.LoadAsync(parameter, token);
        }
    }

    public class LoadFiles<T1, T2> : IDataLoader<T1, T2[]> where T1 : IFileLoadParameter
    {
        private readonly IDataLoader<T1, T2[]> dataLoader;

        public LoadFiles(IDataLoader<T1, T2[]> dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public async UniTask<T2[]> LoadAsync(T1 parameter, CancellationToken token)
        {
            return await dataLoader.LoadAsync(parameter, token);
        }
    }
}
