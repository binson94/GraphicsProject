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
    /// <summary> 박스 열었을 때, 박스 추적하는 기간, 시간 종료 시 원래 위치로 돌아감 </summary>
    const float BOXTIMER = 20;

    /// <summary> 박스 추적하는 남은 기간 </summary>
    float boxTimer = 0;
    Coroutine boxCoroutine = null;

    /// <summary> 길찾기 기능 </summary>
    NavMeshAgent _agent;

    /// <summary> 현재 감지 중인 플레이어 </summary>
    PlayerController _player;

    Vector3 _originPos;

    private void Start()
    {
        _agent= GetComponent<NavMeshAgent>();
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

    #region BoxMove
    /// <summary> 박스 위치로 이동 </summary>
    public void ChaseBox(Vector3 boxPos)
    {
        SetDestination(boxPos);

        boxTimer = BOXTIMER;

        if (boxCoroutine == null)
            boxCoroutine = StartCoroutine(BoxTimerCoroutine());
    }

    /// <summary> 박스 추적 타이머 </summary>
    IEnumerator BoxTimerCoroutine()
    {
        while(boxTimer > 0)
        {
            boxTimer -= 0.25f;
            yield return new WaitForSeconds(0.25f);
        }

        boxCoroutine = null;
    }

    /// <summary> 박스 추적 종료 </summary>
    void StopChaseBox()
    {
        if (boxCoroutine != null)
            StopCoroutine(boxCoroutine);
        boxCoroutine = null;
    }
    #endregion BoxMove

    #region PlayerFollow
    /// <summary> 충돌체에서 플레이어 입장 감지 </summary>
    public void SetTarget(PlayerController player)
    {
        //우선순위 박스보다 높음
        StopChaseBox();
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
