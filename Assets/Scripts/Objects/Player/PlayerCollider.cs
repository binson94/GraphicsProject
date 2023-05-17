/******
작성자 : 박성택
상자 감지 충돌체 클래스
 ******/

using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField]
    PlayerController _player;

    /// <summary> 상자 감지 </summary>
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Box")
        {
            Debug.Log("box");
            Box box = other.GetComponent<Box>();
            _player.FindBox(box);
        }
    }

    /// <summary> 상자 벗어남 </summary>
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Box")
        {
            Box box = other.GetComponent<Box>();
            _player.LostBox(box);
        }
    }
}
