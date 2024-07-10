using NodeGraphProcessor.Examples;
using System;
using UnityEngine;

namespace WobbleShorts
{
    [System.Serializable]
    public abstract class DelayedNode : LinearConditionalNode
    {
        [HideInInspector]
        public Action<DelayedNode> onProcessFinished;
        
        protected void Finish()
        {
            onProcessFinished.Invoke(this);
        }
    }
}