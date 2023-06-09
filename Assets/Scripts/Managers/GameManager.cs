/******
작성자 : 정혁진
전체 게임 컨트롤 클래스
 ******/

using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    static GameManager _instance = null;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
    }
    /// <summary> 최초 접근 시 게임 매니저 생성 </summary>
    static void Init()
    {
        GameObject container = GameObject.Find("@GameManager");
        if (container == null)
            container = new GameObject { name = "@GameManager" };
        _instance = container.AddComponent<GameManager>();
        DontDestroyOnLoad(container);
    }
    #endregion Singleton

    /// <summary> 현재 게임 상태 </summary>
    public Define.GameState GameState { get; private set; } = Define.GameState.Play;

    #region Monster
    /// <summary> 모든 몬스터들 </summary>
    List<MonsterController> _monsters = new List<MonsterController>();
    /// <summary> 몬스터 리스트에 추가 </summary>
    public void AddMonster(MonsterController monster) => _monsters.Add(monster);
    #endregion Monster

    #region Box
    /// <summary> 모든 상자들 </summary>
    List<Box> _boxes = new List<Box>();
    /// <summary> 박스 리스트에 추가 </summary>
    public void AddBox(Box box) => _boxes.Add(box);

    /// <summary> 상자 열었을 때 모든 몬스터 상자로 이동 </summary>
    public void OnBoxOpened(Vector3 boxPosition, bool hasKey)
    {
        foreach (MonsterController mon in _monsters)
            mon.StartChaseBox(boxPosition);

        if (hasKey)
        {
            OnKeyEarn?.Invoke(_currKey);
            _currKey++;
        }
    }
    #endregion Box

    #region Key
    /// <summary> 무작위로 4개 열쇠 생성 </summary>
    void GenerateKey()
    {
        HashSet<int> keySet = new HashSet<int>();

        _maxKey = Mathf.Min(4, _boxes.Count);

        while (keySet.Count < _maxKey)
            keySet.Add(UnityEngine.Random.Range(0, _boxes.Count));

        foreach (Box box in _boxes)
            box.HasKey = false;

        foreach (int idx in keySet)
        {
            _boxes[idx].HasKey = true;

#if UNITY_EDITOR
            Debug.Log(_boxes[idx].name);
#endif
        }
    }

    /// <summary> 현재 열쇠 보유 갯수 </summary>
    int _currKey = 0;
    /// <summary> 탈출에 필요한 열쇠 갯수 </summary>
    int _maxKey;

    /// <summary> 탈출구 열기 가능 여부 </summary>
    public bool CanOpenDoor => _currKey >= _maxKey;
    #endregion Key

    /// <summary> 열쇠 획득 UI 반영 </summary>
    public Action<int> OnKeyEarn { get; set; } = null;
    /// <summary> 일시 정지 UI 닫기 </summary>
    public Action ClosePause { get; set; } = null;
    /// <summary> 게임 오버 UI 띄우기 </summary>
    public Action<bool> OnGameOver { get; set; } = null;

    #region Pause
    /// <summary> 일시 정지 </summary>
    bool _pause { get; set; } = false;

    /// <summary> 일시 정지 토글 </summary>
    public bool TogglePause()
    {
        _pause = !_pause;
        Cursor.visible = _pause;

        if (_pause)
        {
            Time.timeScale = 0;
            Pause();
        }
        else
        {
            Time.timeScale = 1;
            Resume();
        }

        return _pause;
    }
    /// <summary> 게임 일시 정지 </summary>
    public void Pause()
    {
        GameState = Define.GameState.Pause;
        foreach (MonsterController mon in _monsters)
            mon.Pause();
    }
    /// <summary> 게임 재개 </summary>
    public void Resume()
    {
        GameState = Define.GameState.Play;
        foreach (MonsterController mon in _monsters)
            mon.Resume();
    }
    #endregion Pause

    /// <summary> 문 열기 시작 - 게임 승리 </summary>
    public void GameWin()
    {
#if UNITY_EDITOR
        Debug.Log("win");
#endif
        GameState = Define.GameState.Win;

        ClosePause?.Invoke();
    }

    /// <summary> 문 열기 완료 - UI 띄우기 </summary>
    public void ShowWinUI()
    {
        Time.timeScale = 0;
        Cursor.visible = true;

        OnGameOver?.Invoke(true);
        ClosePause?.Invoke();
    }

    /// <summary> 게임 패배 </summary>
    public void GameOver()
    {
        if (GameState == Define.GameState.Win)
            return;

#if UNITY_EDITOR
        Debug.Log("Game Over");
#endif
        GameState = Define.GameState.Lose;
        Time.timeScale = 0;

        Cursor.visible = true;

        OnGameOver?.Invoke(false);
        ClosePause?.Invoke();
    }

    /// <summary> 게임 시작 </summary>
    public void StartGame()
    {
        _currKey = 0;
        GameState = Define.GameState.Play;
        Time.timeScale = 1;
        _pause = false;

        GenerateKey();
    }

    /// <summary> 게임 데이터 초기화 </summary>
    public void Clear()
    {
        ClosePause = null;
        OnGameOver = null;
        OnKeyEarn = null;

        _currKey = 0;

        _monsters.Clear();
        _boxes.Clear();
    }

    public void DEBUG_OPENALL()
    {
        foreach (Box b in _boxes)
            b.Open();
    }
}
