using System;
using UnityEngine;

namespace CTK.Login
{
    [Serializable]
    public class SimpleLoginParameter : ILoginParameter
    {
        [SerializeField]
        private string userId;

        [SerializeField]
        private string password;

        public string Password => password;

        public string UserId => userId;

        public SimpleLoginParameter(string userId, string password)
        {
            this.userId = userId;
            this.password = password;
        }
    }
}
