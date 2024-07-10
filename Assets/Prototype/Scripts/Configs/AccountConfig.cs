using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace WobbleShorts
{
    [CreateAssetMenu(fileName = "AccountConfig", menuName = "WobbleShorts/AccountConfig")]
    public class AccountConfig : ScriptableObject
    {
        [Title("Bio")]
        [SerializeField] private string _accountName;
        public string AccountName => _accountName;
        
        [SerializeField, TextArea] private string _description;
        public string Description => _description;

        [Title("State")]
        [SerializeField] private StateConfig[] _stateConfigs;
        public IReadOnlyList<StateConfig> StateConfigs;
        
        // Todo - set up schedule.
        [FormerlySerializedAs("_clipConfigs")]
        [Title("Posts")]
        [SerializeField] private ShortConfig[] shortConfigs;
        public IReadOnlyList<ShortConfig> ShortConfigs => shortConfigs;
        
        public Account Create()
        {
            Account account = new() 
            {
                Config = this,
                AccountName = _accountName,
                Description = _description,
            };

            List<State> state = new();
            foreach (var config in _stateConfigs) 
                state.Add(config.Create());
       
            account.State = state.ToArray();
            return account;
        }
    }
}