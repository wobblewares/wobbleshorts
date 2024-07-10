#nullable enable

using Sirenix.OdinInspector;
using System.Linq;
using UnityEngine;

namespace WobbleShorts
{
    [System.Serializable]
    public struct EffectConfig
    {
        public enum EffectType
        {
            Both,
            Watch,
            Skip
        }
    
        [Required, SerializeField] private StateConfig state;
        [Required, SerializeField] private AccountConfig account;
        [SerializeField, TableColumnWidth(30, false)] private bool value;
        [SerializeField, TableColumnWidth(100, false)] private EffectType type;
        
        public Effect Create()
        {
            var effect = new Effect();
            var stateConfig = state;
            var accountConfig = account;
            effect.Account = Algorithm.AppState.Accounts.First(x => x.AccountName == accountConfig.AccountName);
            effect.State = effect.Account.State.First(x => x.Identifier == stateConfig.Identifier);
            effect.Value = value;
            effect.Type = type;
            return effect;
        }
    }
}