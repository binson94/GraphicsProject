/******
작성자 : 박성택
상자 감지 충돌체 클래스
 ******/

using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField]
    PlayerController _player;

    /// <summary> 상자 또는 문 감지 </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Box")
        {
#if UNITY_EDITOR
            Debug.Log(other.name);
#endif
            Box box = other.GetComponent<Box>();
            _player.FindBox(box);
        }
        else if(other.gameObject.tag == "Door")
        {
            Door door = other.GetComponent<Door>();
            _player.FindDoor(door);
        }
    }

    /// <summary> 상자 또는 문 벗어남 </summary>
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Box")
        {
            Box box = other.GetComponent<Box>();
            _player.LostBox(box);
        }
        else if(other.gameObject.tag == "Door")
        {
            Door door = other.GetComponent<Door>();
            _player.LostDoor(door);
        }
    }
}
