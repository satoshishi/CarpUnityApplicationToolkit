using System;
using System.Threading;
using CTK.DataEvent;
using Cysharp.Threading.Tasks;

namespace CTK.Login
{
    public class LoginDummyUser : IDataLoader<ILoginParameter, ILoginResult>
    {
        private readonly Func<ILoginParameter, ILoginResult> factory;

        public LoginDummyUser(Func<ILoginParameter, ILoginResult> factory)
        {
            this.factory = factory;
        }

        public UniTask<ILoginResult> LoadAsync(ILoginParameter parameter, CancellationToken token)
        {
            return UniTask.FromResult(factory.Invoke(parameter));
        }
    }
}
