/******
작성자 : 박성택
플레이어 조작 클래스
 ******/
using UnityEngine;
using UnityEngine.UIElements;

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
        if(GameManager.Instance.GameState == Define.GameState.Play)
        {
            SeeMouse();
            Move();
            GetInput();
        }
    }

    #region Move
    float _rotateY;
    float _rotationSpeed = 5f;
    void SeeMouse()
    {
        _rotateY += Input.GetAxis("Mouse X") * _rotationSpeed;

        transform.rotation = Quaternion.Euler(0, _rotateY, 0);

        _cameraController.RotateCamera();
    }

    /// <summary> 입력에 따른 캐릭터 이동 </summary>
    void Move()
    {
        float forwardWeight = Input.GetAxis("Vertical");
        float rightWeight = Input.GetAxis("Horizontal");

        Vector3 moveVector = (Vector3.forward * forwardWeight + Vector3.right * rightWeight).normalized;

        transform.Translate(moveVector * Time.deltaTime * _moveSpeed);
    }
    #endregion Move

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
            Interact();

#if UNITY_EDITOR
        if (Input.GetKey(KeyCode.L))
            GameManager.Instance.DEBUG_OPENALL();
#endif
    }

    #region Box
    Box _nearBox = null;
    Door _nearDoor = null;
    
    /// <summary> 상자 감지 </summary>
    public void FindBox(Box box)
    {
        _nearBox = box;
    }

    /// <summary> 상자 놓침 </summary>
    public void LostBox(Box box)
    {
        if (_nearBox != null && _nearBox == box)
            _nearBox = null;
    }

    /// <summary> 문 감지 </summary>
    public void FindDoor(Door door)
    {
        _nearDoor = door;
    }

    /// <summary> 문 놓침 </summary>
    public void LostDoor(Door door)
    {
        if(_nearDoor != null && _nearDoor == door)
            _nearDoor = null;
    }

    /// <summary> 상자 또는 문 열기 </summary>
    void Interact()
    {
        if (GameManager.Instance.GameState == Define.GameState.Play)
        {
            if (_nearBox != null && _nearBox.Opened == false)
                _nearBox.Open();
            
            if(_nearDoor != null)
                _nearDoor.Open();
        }
    }
    #endregion Box
}
