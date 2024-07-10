using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace WobbleShorts
{
    public class MainUi : MonoBehaviour
    {
        [SerializeField] private RenderTexture _shortsRenderTexture = null;
        private Label _title;
        private Label _description;
        private Label _tags;
        private VisualElement _clipPanel;
        private Image _image;
        
        //Add logic that interacts with the UI controls in the `OnEnable` methods
        private void OnEnable()
        {
            // The UXML is already instantiated by the UIDocument component
            var uiDocument = GetComponent<UIDocument>();
            _title = uiDocument.rootVisualElement.Q("title") as Label;
            _description = uiDocument.rootVisualElement.Q("description") as Label;
            _tags = uiDocument.rootVisualElement.Q("tags") as Label;
            _clipPanel = uiDocument.rootVisualElement.Q("clip-panel") as VisualElement;
            
            // Add a child image
            _image = new Image();
            _image.image = _shortsRenderTexture;
            _clipPanel.Add(_image);
            
            Algorithm.AppState.OnClipShown += SetShort;
        }

        private void OnDisable()
        {
            Algorithm.AppState.OnClipShown -= SetShort;
        }

        public void SetShort(Short @short)
        {
            _title.text = @short.Title;
            _description.text = @short.Description;
            if (@short.Tags.Count > 0)
            {
                _tags.style.visibility = Visibility.Visible;
                _tags.text = $"#{string.Join(" #", @short.Tags)}";
            } 
            else
            {
                _tags.style.visibility = Visibility.Hidden;
            }
        }

        public void SetClip(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}