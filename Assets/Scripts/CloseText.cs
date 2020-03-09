using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CloseText : MonoBehaviour, IPointerClickHandler
{
    Scene scene;
       
    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Menu");
        //Application.Quit();
    }
}
