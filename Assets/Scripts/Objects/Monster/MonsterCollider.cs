/******
작성자 : 이우열
몬스터의 플레이어 감지 콜라이더
 ******/
using UnityEngine;

public class MonsterCollider : MonoBehaviour
{
    /// <summary> 해당 콜라이더를 가진 몬스터 </summary>
    [SerializeField]
    MonsterController _monster;

    float _rayDistance;

    private void Start()
    {
        _rayDistance = GetComponent<SphereCollider>().radius;
    }

    /// <summary> 플레이어 범위 입장 감지 </summary>
    private void OnTriggerStay(Collider other)
    {
        if (_monster.NeedCast)
            if (other.gameObject.tag == "Player")
            {
                if (DoRaycast(other.transform.position) == other.transform)
                    _monster.SetTarget(other.GetComponent<PlayerController>());
            }
    }

    /// <summary> 충돌체 내 플레이어에게 raycasting </summary>
    Transform DoRaycast(Vector3 pos)
    {
        //벽과 플레이어만 감지
        int mask = (1 << 4);

        Vector3 currPos = transform.position + Vector3.up;

        Ray ray = new Ray(currPos, (pos - currPos).normalized);

        RaycastHit hit;
        Debug.DrawRay(transform.position, pos - transform.position, Color.red, 2f);

        if (Physics.Raycast(ray, out hit, _rayDistance, mask))
        {
            Debug.Log(hit.transform.gameObject.name);
            return hit.transform;

        }

        return null;
    }

    /// <summary> 플레이어 범위 퇴장 감지 </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
            _monster.LostTarget();
    }
}
