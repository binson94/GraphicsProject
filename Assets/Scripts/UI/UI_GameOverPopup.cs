/******
작성자 : 
게임 오버 UI
 ******/

using UnityEngine;

public class UI_GameOverPopup : MonoBehaviour
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
