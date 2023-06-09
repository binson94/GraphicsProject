/******
작성자 : 이우열
몬스터 조작 클래스
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

    /// <summary> 느낌표 마크 </summary>
    [Header("Model"), SerializeField]
    GameObject _exclamationMark;
    /// <summary> 효과음 재생을 위한 소스 </summary>
    [SerializeField]
    AudioSource _audioSource;

    /// <summary> 몬스터의 현재 상태 </summary>
    MonsterState _state = MonsterState.Patrol;
    /// <summary> 몬스터의 현재 상태 </summary>
    MonsterState State
    {
        get => _state;
        set
        {
            _exclamationMark.SetActive(value == MonsterState.PlayerChase);
            if (value == MonsterState.BoxChase)
                _agent.speed = 3f;
            else
                _agent.speed = 2f;

            _state = value;
        }
    }
    /// <summary> 길찾기 기능 </summary>
    NavMeshAgent _agent;

    #region Patrol
    /// <summary> 순찰 시 목적지 리스트 </summary>
    [Header("Patrol"), SerializeField]
    List<Vector3> patrolPos = new List<Vector3>();
    /// <summary> 순찰 시 현재 목적지 index </summary>
    int _patrolIdx = 0;
    #endregion Patrol

    #region BoxChase
    /// <summary> 박스 열었을 때, 박스 추적하는 기간, 시간 종료 시 원래 위치로 돌아감 </summary>
    const float TIMER_START = 10;
    /// <summary> 박스 추적하는 남은 기간 </summary>
    float remainTimer = 0;
    #endregion BoxChase

    #region PlayerChase
    /// <summary> 현재 감지 중인 플레이어 </summary>
    PlayerController _player;

    /// <summary> raycast 필요 여부 반환 </summary>
    public bool NeedCast { get => _player == null; }
    #endregion PlayerChase

    private void Awake()
    {
        GameManager.Instance.AddMonster(this);
    }

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();

        patrolPos.Insert(0, transform.position);
    }

    private void Update()
    {
        MoveByState();
    }

    /// <summary> 현재 상태에 따른 행동 결정 </summary>
    void MoveByState()
    {
        switch (State)
        {
            case MonsterState.Patrol:
                ArriveCheck();
                break;
            case MonsterState.BoxChase:
                BoxTimer();
                break;
            case MonsterState.PlayerChase:
                if (_player != null)
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
        if (Vector3.Distance(transform.position, _agent.destination) <= 0.01f)
        {
            int nextIdx = (_patrolIdx + 1) % patrolPos.Count;
            StartPatrol(nextIdx);
        }
    }

    /// <summary> 순찰 시작 </summary>
    void StartPatrol(int posIdx)
    {
        State = MonsterState.Patrol;
        _patrolIdx = posIdx;
        SetDestination(patrolPos[posIdx]);
    }

    #endregion Patrol

    #region BoxChase
    /// <summary> 박스 위치로 이동 </summary>
    public void StartChaseBox(Vector3 boxPos)
    {
        //플레이어 추적이 우선순위 더 높음
        if (State == MonsterState.PlayerChase)
            return;

        State = MonsterState.BoxChase;
        SetDestination(boxPos);

        //idempotent 하도록 설정
        remainTimer = TIMER_START;
    }

    /// <summary> 박스 추적 타이머 </summary>
    void BoxTimer()
    {
        remainTimer -= Time.deltaTime;

        if (remainTimer <= 0)
            StartPatrol(0);
    }
    #endregion BoxChase

    #region PlayerChase
    /// <summary> 충돌체에서 플레이어 입장 감지 </summary>
    public void SetTarget(PlayerController player)
    {
        SoundManager.Play(_audioSource, Define.SFX.MonsterFound);
        State = MonsterState.PlayerChase;
        _player = player;
    }

    /// <summary> 충돌체에서 플레이어 퇴장 감지 -> 원래 위치로 이동 </summary>
    public void LostTarget()
    {
        State = MonsterState.Patrol;
        _player = null;

        StartPatrol(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            GameManager.Instance.GameOver();
    }
    #endregion PlayerChase
}
