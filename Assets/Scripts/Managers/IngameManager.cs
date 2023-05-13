/******
작성자 : 정혁진
작성 일자 : 23.05.13
 ******/
using UnityEngine;

public class IngameManager
{
    public void GameOver()
    {
        Debug.Log("Game Over");
        Time.timeScale = 0;
    }
}
