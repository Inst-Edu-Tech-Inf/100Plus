using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void PlayBtnClick()
    {
        SceneManager.LoadScene("Game");
    }

    public void MultiplayerBtnClick()
    {

    }

    public void ExitBtnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
