using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using GraphProcessor;
using System.Reflection;

namespace WobbleShorts
{
    [NodeCustomEditor(typeof(PlayClipVideoNode))]
    public class PlayClipVideoNodeView : BaseNodeView
    {

        private VisualElement _loopField;
        private const string _previewElementName = "preview_image";
        
        public override void Enable()
        {
            base.Enable();
            owner.graph.onGraphChanges += (_) =>
            {
                RefreshPreviewImage();
            };
        }
        
        protected override void DrawDefaultInspector(bool fromInspector = false)
        {
            var node = nodeTarget as PlayClipVideoNode;

            AddControlField(nameof(PlayClipVideoNode.executed), label: null, showInputDrawer: true);
            AddControlField(nameof(PlayClipVideoNode.clip), label: null, showInputDrawer: true);
            AddControlField(nameof(PlayClipVideoNode.loop), label: null, showInputDrawer: true, () =>
            {
                if (node.loop)
                {
                    var view = GetFirstPortViewFromFieldName(nameof(PlayClipVideoNode.duration));
                    _loopField = AddControlField(nameof(PlayClipVideoNode.duration), label: null, showInputDrawer: true);
                    view.SetEnabled(true);
                } 
                else
                {
                    var view = GetFirstPortViewFromFieldName(nameof(PlayClipVideoNode.duration));
                    if (_loopField != null)
                        inputContainerElement.Remove(_loopField.parent);
                    view.SetEnabled(false);
                }
            });

            RefreshPreviewImage();
        }
        
        private void RefreshPreviewImage()
        {
            var node = nodeTarget as PlayClipVideoNode;
            
            // Create your fields using node's variables and add them to the controlsContainer
            var previewImage = outputContainer.Q<Image>(_previewElementName) ?? new Image() { name = _previewElementName };
            previewImage.style.visibility = node.clip == null ? Visibility.Hidden : Visibility.Visible;
            if (node.clip == null)
            {
                previewImage.image = null;
                return;
            }
            
            var texture = AssetPreview.GetMiniThumbnail(node.clip);
            if (texture != null)
            {
                var width = 100;
                previewImage.scaleMode = ScaleMode.StretchToFill;
                float newHeight = (texture.height / (float)texture.width) * width;
                previewImage.style.width = width;
                previewImage.style.height = newHeight;
                previewImage.image = texture;
            } 
            else
            {
                previewImage.image = Texture2D.blackTexture;
            }

            if (!outputContainer.Contains(previewImage))
                outputContainer.Add(previewImage);
        }
    }
}