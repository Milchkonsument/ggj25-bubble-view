using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    public GameObject playIcon;
    [SerializeField]
    private CanvasGroup _menuCanvas;

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(_menuCanvas.alpha == 1)
        {
            playIcon.SetActive(true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        playIcon.SetActive(false);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (_menuCanvas.alpha == 1)
        {
            playIcon.SetActive(true);
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        playIcon.SetActive(false);
    }
}
