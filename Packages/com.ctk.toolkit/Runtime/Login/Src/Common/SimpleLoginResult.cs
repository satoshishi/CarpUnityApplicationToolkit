using System;
using UnityEngine;

namespace CTK.Login
{
    [Serializable]
    public class SimpleLoginResult : ILoginResult
    {
        [SerializeField]
        private bool successful;

        [SerializeField]
        private string loginUserId;

        public bool Successful => successful;

        public string LoginUserId => loginUserId;

        public SimpleLoginResult(bool successful, string loginUiserId)
        {
            this.successful = successful;
            this.loginUserId = loginUiserId;
        }
    }
}
