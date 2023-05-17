using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_GameOverPopup : MonoBehaviour
{
    public void Btn_Retry()
    {
        SceneManager.LoadScene(1);
    }

    public void Btn_Exit()
    {
        SceneManager.LoadScene(0);
    }
}
