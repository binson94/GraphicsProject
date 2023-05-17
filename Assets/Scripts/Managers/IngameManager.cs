/******
작성자 : 정혁진
작성 일자 : 23.05.13
 ******/
using System;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager
{
    /// <summary> 현재 게임 상태 </summary>
    public Define.GameState GameState { get; private set; } = Define.GameState.Play;

    /// <summary> 모든 몬스터들 </summary>
    List<MonsterController> _monsters = new List<MonsterController>();

    public Action ClosePause = null;

    /// <summary> 몬스터 리스트에 추가 </summary>
    public void AddMonster(MonsterController monster) => _monsters.Add(monster);

    /// <summary> 상자 열었을 때 모든 몬스터 상자로 이동 </summary>
    public void OnBoxOpened(Vector3 boxPosition)
    {
        foreach (MonsterController mon in _monsters)
            mon.StartChaseBox(boxPosition);
    }

    /// <summary> 게임 일시 정지 </summary>
    public void Pause()
    {
        GameState = Define.GameState.Pause;
        foreach(MonsterController mon in _monsters)
            mon.Pause();
    }

    /// <summary> 게임 재개 </summary>
    public void Resume()
    {
        GameState = Define.GameState.Play;
        foreach(MonsterController mon in _monsters) 
            mon.Resume();
    }

    /// <summary> 게임 패배 </summary>
    public void GameOver()
    {
        Debug.Log("Game Over");
        GameState = Define.GameState.Lose;
        Time.timeScale = 0;

        ClosePause?.Invoke();
    }

    /// <summary> 게임 데이터 초기화 </summary>
    public void Clear()
    {
        _monsters.Clear();
    }
}
