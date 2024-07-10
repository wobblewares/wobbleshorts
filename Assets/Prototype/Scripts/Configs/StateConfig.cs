#nullable enable

using UnityEngine;

namespace WobbleShorts
{
    [CreateAssetMenu(fileName = "StateConfig", menuName = "WobbleShorts/StateConfig")]
    public class StateConfig : ScriptableObject
    {
        [SerializeField] private string _identifier;
        public string Identifier => _identifier;
        
        [SerializeField] private bool _value;
        public bool Value => _value;

        public State Create()
        {
            return new()
            {
                Identifier = _identifier, 
                Value = _value
            };
        }
    }
}