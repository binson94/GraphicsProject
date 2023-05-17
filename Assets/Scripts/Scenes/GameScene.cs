/******
작성자 : 
게임 중 UI 관리 클래스
 ******/
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    bool _pause = false;

    [SerializeField]
    UI_PausePopup _pausePopup;

    private void Start()
    {
        Cursor.visible = false;
        _pausePopup.gameObject.SetActive(false);

        GameManager.Ingame.ClosePause = () => gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Ingame.GameState <= Define.GameState.Pause)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                TogglePause();
            }
    }

    /// <summary> 일시 정지 토글 </summary>
    void TogglePause()
    {
        _pause = !_pause;
        Cursor.visible = _pause;
        _pausePopup.gameObject.SetActive(_pause);

        if(_pause)
        {
            Time.timeScale = 0;
            GameManager.Ingame.Pause();
        }
        else
        {
            Time.timeScale = 1;
            GameManager.Ingame.Resume();
        }
    }
}
