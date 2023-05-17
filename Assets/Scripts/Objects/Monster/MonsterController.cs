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
    enum MonsterState
    {
        Patrol,
        BoxChase,
        PlayerChase,
    }

    /// <summary> 몬스터의 현재 상태 </summary>
    MonsterState state { get; set; } = MonsterState.Patrol;
    /// <summary> 길찾기 기능 </summary>
    NavMeshAgent _agent;

    #region Patrol
    /// <summary> 순찰 시 목적지 리스트 </summary>
    [Header("Patrol")]
    [SerializeField]
    List<Vector3> patrolPos = new List<Vector3>();
    /// <summary> 순찰 시 현재 목적지 index </summary>
    int _patrolIdx = 0;
    #endregion Patrol

    #region BoxChase
    /// <summary> 박스 열었을 때, 박스 추적하는 기간, 시간 종료 시 원래 위치로 돌아감 </summary>
    const float BOXTIMER = 20;
    /// <summary> 박스 추적하는 남은 기간 </summary>
    float boxTimer = 0;
    /// <summary> 박스 추적 타이머 코루틴 </summary>
    Coroutine boxCoroutine = null;
    #endregion BoxChase

    #region PlayerChase
    /// <summary> 현재 감지 중인 플레이어 </summary>
    PlayerController _player;
    #endregion PlayerChase

    private void Awake()
    {
        GameManager.Ingame.AddMonster(this);
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        patrolPos.Insert(0, transform.position);

        //DEBUG
        patrolPos.Add(transform.position + Vector3.forward * 5);
    }

    private void Update()
    {
        MoveByState();
    }

    /// <summary> 현재 상태에 따른 행동 결정 </summary>
    void MoveByState()
    {
        switch (state)
        {
            case MonsterState.Patrol:
                ArriveCheck();
                break;
            //case MonsterState.BoxChase:
            //    //DO NOTHING
            //    break;
            case MonsterState.PlayerChase:
                if(_player != null)
                SetDestination(_player.transform.position);
                break;
        }
    }

    /// <summary> 해당 위치로 이동 지시 </summary>
    public void SetDestination(Vector3 dest)
    {
        _agent.SetDestination(dest);
    }

    public void Pause()
    {
        _agent.speed = 0;
    }

    public void Resume()
    {
        _agent.speed = 1;
    }

    #region Patrol
    /// <summary> 현재 순찰 목적지에 도착했는 지 검사 </summary>
    void ArriveCheck()
    {
        if (_agent.remainingDistance <= 0.01f)
        {
            int nextIdx = (_patrolIdx + 1) % patrolPos.Count;
            StartPatrol(nextIdx);
        }
    }

    /// <summary> 순찰 시작 </summary>
    void StartPatrol(int posIdx)
    {
        state = MonsterState.Patrol;
        _patrolIdx = posIdx;
        SetDestination(patrolPos[posIdx]);
    }

    #endregion Patrol

    #region BoxChase
    /// <summary> 박스 위치로 이동 </summary>
    public void StartChaseBox(Vector3 boxPos)
    {
        //플레이어 추적이 우선순위 더 높음
        if (state == MonsterState.PlayerChase)
            return;

        SetDestination(boxPos);

        //idempotent 하도록 설정
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
        StartPatrol(0);
    }

    /// <summary> 박스 추적 종료 </summary>
    void StopChaseBox()
    {
        if (boxCoroutine != null)
            StopCoroutine(boxCoroutine);
        boxCoroutine = null;
    }
    #endregion BoxChase

    #region PlayerChase
    /// <summary> 충돌체에서 플레이어 입장 감지 </summary>
    public void SetTarget(PlayerController player)
    {
        state = MonsterState.PlayerChase;
        //우선순위 박스보다 높음
        StopChaseBox();
        _player = player;
    }

    /// <summary> 충돌체에서 플레이어 퇴장 감지 -> 원래 위치로 이동 </summary>
    public void LostTarget()
    {
        state = MonsterState.Patrol;
        _player = null;

        StartPatrol(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            GameManager.Ingame.GameOver();
    }
    #endregion PlayerChase
}
