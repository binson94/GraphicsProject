/******
작성자 : 정혁진
작성 일자 : 23.05.06
 ******/

using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    static GameManager _instance = null;
    static GameManager Instance
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

    IngameManager _ingame = new IngameManager();
    public static IngameManager Ingame => Instance._ingame;
}
