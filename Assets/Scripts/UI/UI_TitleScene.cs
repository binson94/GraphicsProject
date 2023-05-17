/******
작성자 : 이우열
작성 일자 : 23.05.17
 ******/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        SceneManager.LoadScene(1);
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
