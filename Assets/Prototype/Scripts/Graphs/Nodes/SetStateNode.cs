using Cysharp.Threading.Tasks;
using GraphProcessor;
using NodeGraphProcessor.Examples;
using System;
using UnityEngine;

namespace WobbleShorts
{
    [Serializable, NodeMenuItem("WobbleShorts/Set State Node")]
    public class SetStateNode : LinearConditionalNode
    {
        public override string name => "Set State";

        [Input("Account"), SerializeField]
        public AccountConfig accountConfig = null;
        
        [Input("State"), SerializeField]
        public StateConfig stateConfig = null;

        [Input("Value"), SerializeField]
        public bool value = true;
        
        protected override void Process()
        {
            var account = Algorithm.AppState.GetAccount(accountConfig);
            if (account == null)
            {
                Debug.LogError($"No active account for {accountConfig.AccountName} was found.");
                return;
            }

            // Update state.
            var state = account.Value.GetState(stateConfig);
            if (state != null && state.Value is bool val)
                state.Value = value;
        }
    }
}