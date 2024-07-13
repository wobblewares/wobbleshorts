using UnityEngine;

namespace WobbleShorts
{
    public class QuickStart : MonoBehaviour
    {
        public AccountConfig _startingAccount;
        public ShortConfig _startingShort;

        private void Start()
        {
            var app = Algorithm.AppState;
            if (Algorithm.AppState.GetAccount(_startingAccount) is {} account)
            {
                if (_startingShort != null)
                {
                    var @short = _startingShort.Create(account);
                    app.Post(@short);
                    app.Show(@short);
                }
            }
        }
    }
}