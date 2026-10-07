using System.Threading;
using CTK.DataEvent;
using Cysharp.Threading.Tasks;

namespace CTK.Login
{
    public class LoginUser : IDataLoader<ILoginParameter, ILoginResult>
    {
        private readonly IDataLoader<ILoginParameter, ILoginResult> requestLogin;

        private readonly IDataReactiveProperty<ILoginResult> loginResult;

        public LoginUser(IDataLoader<ILoginParameter, ILoginResult> requestLogin, IDataReactiveProperty<ILoginResult> loginResult)
        {
            this.requestLogin = requestLogin;
            this.loginResult = loginResult;
        }

        public async UniTask<ILoginResult> LoadAsync(ILoginParameter parameter, CancellationToken token)
        {
            ILoginResult result = await requestLogin.LoadAsync(parameter, token);

            loginResult.Value = result;

            return result;
        }
    }
}
