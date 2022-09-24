using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Moonee
{
    public class TextLink : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Canvas canvas;

        public void OnPointerClick(PointerEventData eventData)
        {
            var linkIndex = TMP_TextUtilities.FindIntersectingLink(text, eventData.position, Camera);
            if (linkIndex != -1)
            {
                var linkInfo = text.textInfo.linkInfo[linkIndex];
                Application.OpenURL(linkInfo.GetLinkID());
            }
        }

        private Camera Camera => canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;

    }
}
