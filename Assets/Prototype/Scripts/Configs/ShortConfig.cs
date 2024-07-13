#nullable enable

using GraphProcessor;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WobbleShorts
{
    [CreateAssetMenu(fileName = "ShortConfig", menuName = "WobbleShorts/New ShortConfig")]
    public class ShortConfig : ScriptableObject
    {
        [Title("Details")]
        [SerializeField] private string _title = null!;
        public string Title => _title;
        
        [SerializeField, TextArea] private string _description = null!;
        public string Description => _description;
        
        [Title("Metadata")]
        [SerializeField] private List<string> _tags = null!;
        public IReadOnlyList<string> Tags => _tags;
        
        [Title("State")]
        // TODO - Requirements & Effects need to be on the 'post' schedule so they
        // can be configured per account, allowing clips to be shared across accounts
        // (or included in multiple parts of a schedule).
        [TableList]
        [SerializeField] private List<RequirementConfig> _requirements = null!;
        
        [TableList]
        [SerializeField] private List<EffectConfig> _effects = null!;

        [Title("Graph")]
        [SerializeField] private ShortGraph _graph = null!;

#if UNITY_EDITOR
        [EnableIf(nameof(ValidateGraph))]
        [Button(ButtonSizes.Small, Name = "Edit Graph")]
        void EditGraph() => EditorWindow.GetWindow<ShortGraphWindow>().InitializeGraph(_graph);
#endif        

        bool ValidateGraph() => _graph != null;
        public ShortGraph Graph => _graph;
        
        public enum Status
        {
            Draft,
            Published,
            Watched,
            Skipped,
            Cancelled
        }

        public Short Create(Account author)
        {
            var clip = new Short()
            {
                Title = _title,
                Description = _description,
                Tags = _tags,
                Status = Status.Draft,
                Author = author,
                Graph = _graph
            };
            
            // Copy ongoing requirements
            List<Requirement> requirements = new();
            foreach (var config in _requirements)
            {
                if (config.Type == RequirementConfig.RequirementType.Always)
                    requirements.Add(config.Create());
            }
            clip.Requirements = requirements.ToArray();
            
            // Copy effects
            List<Effect> effects = new();
            foreach (var effect in _effects)
                effects.Add(effect.Create());
            clip.Effects = effects.ToArray();
            
            return clip;
        }
    }
}