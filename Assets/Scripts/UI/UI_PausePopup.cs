using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PausePopup : MonoBehaviour
{
    public void Btn_Retry()
    {
        SceneManager.LoadScene(Define.Scene.Ingame);
    }

    public void Btn_Exit()
    {
        SceneManager.LoadScene(Define.Scene.Title);
    }
}
