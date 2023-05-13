/******
작성자 : 이우열
작성 일자 : 23.05.13
 ******/
using UnityEngine;

public class MonsterCollider : MonoBehaviour
{
    /// <summary> 해당 콜라이더를 가진 몬스터 </summary>
    [SerializeField]
    MonsterController _monster;

    /// <summary> 플레이어 범위 입장 감지 </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _monster.SetTarget(other.GetComponent<PlayerController>());
    }

    /// <summary> 플레이어 범위 퇴장 감지 </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _monster.LostTarget();
    }
}
