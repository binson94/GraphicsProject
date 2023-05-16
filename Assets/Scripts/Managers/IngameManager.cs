/******
작성자 : 정혁진
작성 일자 : 23.05.13
 ******/
using System.Collections.Generic;
using UnityEngine;

public class IngameManager
{
    /// <summary> 현재 게임 상태 </summary>
    public Define.GameState GameState { get; private set; } = Define.GameState.Play;

    /// <summary> 모든 몬스터들 </summary>
    List<MonsterController> _monsters = new List<MonsterController>();

    /// <summary> 상자 열었을 때 모든 몬스터 상자로 이동 </summary>
    public void OnBoxOpened(Vector3 boxPosition)
    {
        foreach (MonsterController mon in _monsters)
            mon.ChaseBox(boxPosition);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        GameState = Define.GameState.Lose;
        Time.timeScale = 0;
    }
}
