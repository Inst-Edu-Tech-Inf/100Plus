using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
//using UnityEngine.UI.Module;
using UnityEngine.Networking;
//using System.Collections;
using System.Collections;
//using ConfirmationDialog;

public class CloseText : MonoBehaviour, IPointerClickHandler
{
    Scene scene;
    //public ConfirmationDialog confirmationDialog;
    bool isQuit = false;
       
    public void OnPointerClick(PointerEventData eventData)
    {
        //StartCoroutine(AskQuit());
        SceneManager.LoadScene("Menu");
        //Application.Quit();

 
    
    }

   /* IEnumerator AskQuit()
    {
        // whatever you're doing now with the temporary / placement preview building

        ConfirmationDialog dialog = Instantiate(confirmationDialog, this.canvas.transform); // instantiate the UI dialog box

        while (dialog.result == dialog.NONE)
            yield return null; // wait

        if (dialog.result == dialog.YES)
        {
            // place the real building
        }
        else if (dialog.result == dialog.CANCEL)
        {
            // remove the temporary / preview building
        }
    }*/

    void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 128, 30), "BUTTON"))
        {
            isQuit = true;
        }

        if (isQuit)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            GUI.Button(new Rect(200, 210, 200, 30), "YES");
            GUI.Button(new Rect(200, 240, 200, 30), "NO");
        }
    }

}
