#nullable enable

using GraphProcessor;
using NodeGraphProcessor.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace WobbleShorts
{
    public class SampleItemNode<T> : LinearConditionalNode
    {
        public override string name => "Sample Item Node";

        [Input("Items"), SerializeField]
        public WeightedListConfig<T> ItemListConfig = null;
        
        [Output(name = "Item")]
        public T? item;

        protected override void Process()
        {
            int totalWeight = ItemListConfig.Items.Sum(i => i.Weight);
            int randomWeight = UnityEngine.Random.Range(0, totalWeight);

            // Find the item that this random value falls on
            int cumulativeWeight = 0;
            foreach (var i in ItemListConfig.Items)
            {
                cumulativeWeight += i.Weight;
                
                if (randomWeight < cumulativeWeight)
                {
                    item = i.Value;
                    break;
                }
            }
        }
    }
    
    [Serializable, NodeMenuItem("WobbleShorts/Sample String Node")]
    public class SampleStringNode : SampleItemNode<string> {
        public override string name => "Sample String Node";
    }
}