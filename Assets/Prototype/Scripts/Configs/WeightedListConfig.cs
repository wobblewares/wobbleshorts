using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace WobbleShorts
{
    public class WeightedListConfig<T> : ScriptableObject
    {
        [SerializeField] private WeightedValue[] _items;
        public IReadOnlyList<WeightedValue> Items => _items;
        
        [System.Serializable]
        public struct WeightedValue
        {
            [HorizontalGroup("WeightedValue", width: 50), HideLabel]
            public int Weight;
            [HorizontalGroup("WeightedValue"), HideLabel]
            public T Value;
        }
    }
}