using System.Threading;
using Cysharp.Threading.Tasks;

namespace CTK.DataEvent
{
    public interface IDataLoader<T>
    {
        UniTask<T> LoadAsync(CancellationToken token);
    }

    public interface IDataLoader<T1, T2>
    {
        UniTask<T2> LoadAsync(T1 parameter, CancellationToken token);
    }
}
