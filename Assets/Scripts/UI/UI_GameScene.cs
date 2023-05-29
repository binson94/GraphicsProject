/******
작성자 : 이우열
인게임 UI 관리 클래스
 ******/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameScene : MonoBehaviour
{
    /// <summary> 일시정지 팝업 </summary>
    [Header("ui")]
    [SerializeField]
    UI_PausePopup _pausePopup;
    /// <summary> 게임 승리 팝업 </summary>
    [SerializeField]
    GameObject _winPopup;
    /// <summary> 게임 오버 팝업 </summary>
    [SerializeField]
    UI_GameOverPopup _gameOverPopup;

    /// <summary> 획득 열쇠 표기 이미지 </summary>
    [SerializeField]
    List<Image> _keyImages;

    private void Start()
    {
        Cursor.visible = false;
        _pausePopup.gameObject.SetActive(false);
        _winPopup.SetActive(false);
        _gameOverPopup.gameObject.SetActive(false);

        GameManager.Instance.ClosePause = () => _pausePopup.gameObject.SetActive(false);
        GameManager.Instance.OnGameOver += OnGameOver;
        GameManager.Instance.OnKeyEarn += OnKeyEarned;

        GameManager.Instance.StartGame();
    }

    /// <summary> esc 입력 감지하여 일시 정지 </summary>
    void Update()
    {
        if (GameManager.Instance.GameState == Define.GameState.Play || GameManager.Instance.GameState == Define.GameState.Pause)
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseToggle();
    }

    /// <summary> 열쇠 획득 시 이미지 적용 </summary>
    void OnKeyEarned(int idx)
    {
        if (idx >= _keyImages.Count)
            return;

        _keyImages[idx].sprite = Resources.Load<Sprite>("Images/key_color");
    }

    /// <summary> 일시 정지 토글 </summary>
    void PauseToggle()
    {
        bool isPause = GameManager.Instance.TogglePause();
        _pausePopup.gameObject.SetActive(isPause);
    }

    public void Resume() => PauseToggle();

    /// <summary> 게임 오버 시 UI 띄움 </summary>
    void OnGameOver(bool isWin)
    {
        if (isWin)
            _winPopup.SetActive(true);
        else
            _gameOverPopup.gameObject.SetActive(true);
    }
}
