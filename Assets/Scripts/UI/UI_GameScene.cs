/******
작성자 : 
인게임 UI 관리 클래스
 ******/

using UnityEngine;

public class UI_GameScene : MonoBehaviour
{
    /// <summary> 일시정지 팝업 </summary>
    [Header("ui")]
    [SerializeField]
    UI_PausePopup _pausePopup;
    /// <summary> 게임 오버 팝업 </summary>
    [SerializeField]
    UI_GameOverPopup _gameOverPopup;

    private void Start()
    {
        Cursor.visible = false;
        _pausePopup.gameObject.SetActive(false);
        _gameOverPopup.gameObject.SetActive(false);

        GameManager.Instance.ClosePause = () => _pausePopup.gameObject.SetActive(false);
        GameManager.Instance.OnGameOver += OnGameOver;

        GameManager.Instance.StartGame();
    }

    /// <summary> esc 입력 감지하여 일시 정지 </summary>
    void Update()
    {
        if (GameManager.Instance.GameState == Define.GameState.Play || GameManager.Instance.GameState == Define.GameState.Pause)
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseToggle();
    }

    /// <summary> 일시 정지 토글 </summary>
    void PauseToggle()
    {
        bool isPause = GameManager.Instance.TogglePause();
        _pausePopup.gameObject.SetActive(isPause);
    }

    /// <summary> 게임 오버 시 UI 띄움 </summary>
    void OnGameOver()
    {
        _gameOverPopup.gameObject.SetActive(true);
    }
}
