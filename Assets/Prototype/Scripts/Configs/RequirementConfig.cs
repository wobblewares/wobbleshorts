using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace WobbleShorts
{
    [System.Serializable]
    public struct RequirementConfig
    {
        public enum RequirementType
        {
            Initial,
            Always
        }
        
        [SerializeField, Required] private StateConfig state;
        [SerializeField, Required] private AccountConfig account;
        [SerializeField, TableColumnWidth(30, false)] private bool value;
        
        [SerializeField, TableColumnWidth(100, false)] private RequirementType type;
        public RequirementType Type => type;
        
        public Requirement Create()
        {
            var requirement = new Requirement();
            var stateConfig = state;
            var accountConfig = account;
            requirement.Account = Algorithm.AppState.Accounts.First(x => x.AccountName == accountConfig.AccountName);
            requirement.State = requirement.Account.State.First(x => x.Identifier == stateConfig.Identifier);
            requirement.Value = value;
            return requirement;
        }
    }
}