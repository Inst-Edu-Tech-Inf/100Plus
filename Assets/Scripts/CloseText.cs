using UnityEngine;
using UnityEngine.EventSystems;

public class CloseText : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.Quit();
    }
}
