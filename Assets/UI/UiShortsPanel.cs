using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace WobbleShorts
{
    public class UiShortsPanel : MonoBehaviour
    {
        [SerializeField, Required] private Image image;

        public void SetClip(Sprite sprite)
        {
            image.sprite = sprite;
        }
    }
}