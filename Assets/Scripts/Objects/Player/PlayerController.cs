/******
작성자 : 박성택
작성 일자 : 23.05.13
 ******/
using UnityEngine;

/// <summary> 플레이어 조작을 위한 클래스 </summary>
public class PlayerController : MonoBehaviour
{
    /// <summary> 카메라 회전 제어 클래스 </summary>
    CameraController _cameraController = new CameraController();

    /// <summary> 플레이어 이동 속도 </summary>
    [SerializeField]
    float _moveSpeed = 5f;

    /// <summary> 카메라 컨트롤러 설정 </summary>
    private void Start()
    {
        _cameraController.Init(Camera.main);
    }

    /// <summary> 매 프레임마다 카메라 회전, 캐릭터 이동 업데이트 </summary>
    void Update()
    {
        if(GameManager.Ingame.GameState == Define.GameState.Play)
        {
            _cameraController.RotateCamera();
            Move();
        }
    }

    /// <summary> 입력에 따른 캐릭터 이동 </summary>
    void Move()
    {
        float forwardWeight = Input.GetAxis("Vertical");
        float rightWeight = Input.GetAxis("Horizontal");

        //카메라 회전에 따른 방향 획득
        Vector3 moveVector = _cameraController.GetWeightedDirection(forwardWeight, rightWeight);

        transform.Translate(moveVector * Time.deltaTime * _moveSpeed);
    }
}
