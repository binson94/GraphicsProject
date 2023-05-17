/******
작성자 : 
타이틀 씬 UI
 ******/

using UnityEngine;

public class UI_TitleScene : MonoBehaviour
{
    [SerializeField]
    UI_HelpPopup _helpPopup;

    private void Start()
    {
        _helpPopup.gameObject.SetActive(false);
    }

    /// <summary> 게임 시작 버튼 </summary>
    public void Btn_Start()
    {
        SceneManager.LoadScene(Define.Scene.Ingame);
    }

    /// <summary> 도움말 버튼 </summary>
    public void Btn_Help()
    {
        _helpPopup.gameObject.SetActive(true);
        _helpPopup.Open();
    }

    /// <summary> 게임 종료 버튼 </summary>
    public void Btn_Exit()
    {
        Application.Quit();
    }
}
