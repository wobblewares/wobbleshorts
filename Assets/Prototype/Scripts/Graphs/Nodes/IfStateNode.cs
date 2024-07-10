#nullable enable

using GraphProcessor;
using NodeGraphProcessor.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WobbleShorts
{
    [Serializable, NodeMenuItem("WobbleShorts/If State Node")]
    public class IfStateNode : ConditionalNode
    {
        public override string name => "If State";

        [Input("Account"), SerializeField]
        public AccountConfig accountConfig = null;
        
        [Input("State"), SerializeField]
        public StateConfig stateConfig = null;

        [Input("Value"), SerializeField]
        public bool value = true;
        
        [Output(name = "True")]
        public ConditionalLink	@true;
        [Output(name = "False")]
        public ConditionalLink	@false;

        public override IEnumerable<ConditionalNode> GetExecutedNodes()
        {
            var account = Algorithm.AppState.GetAccount(accountConfig);
            if (account == null)
            {
                Debug.LogError($"No active account for {accountConfig.AccountName} was found.");
                return null;
            }

            var state = account.Value.GetState(stateConfig);
            if (state != null && state.Value is bool val)
            {
                var fieldName = val == value ? nameof(@true) : nameof(@false);
                return outputPorts.FirstOrDefault(n => n.fieldName == fieldName)
                    .GetEdges().Select(e => e.inputNode as ConditionalNode);
            }
            return null;
        }
    }
}