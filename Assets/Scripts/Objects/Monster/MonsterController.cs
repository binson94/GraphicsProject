/******
작성자 : 이우열
작성 일자 : 23.05.09
 ******/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    /// <summary> 길찾기 기능 </summary>
    [SerializeField]
    NavMeshAgent _agent;

    /// <summary> 현재 감지 중인 플레이어 </summary>
    PlayerController _player;

    Vector3 _originPos;

    private void Start()
    {
        _originPos = transform.position;
    }

    private void Update()
    {
        if (_player != null)
            SetDestination(_player.transform.position);
    }

    /// <summary> 해당 위치로 이동 지시 </summary>
    public void SetDestination(Vector3 dest)
    {
        _agent.SetDestination(dest);
    }

    #region PlayerFollow
    /// <summary> 충돌체에서 플레이어 입장 감지 </summary>
    public void SetTarget(PlayerController player)
    {
        _player = player;
    }

    /// <summary> 충돌체에서 플레이어 퇴장 감지 -> 원래 위치로 이동 </summary>
    public void LostTarget()
    {
        _player = null;
        SetDestination(_originPos);
    }
    #endregion

    #region PlayerKill
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            GameManager.Ingame.GameOver();
    }
    #endregion PlayerKill
}
