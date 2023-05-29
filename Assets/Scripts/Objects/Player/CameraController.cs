/******
작성자 : 박성택
카메라 회전 제어 클래스
 ******/
using UnityEngine;

/// <summary> 1인칭 카메라 회전 제어를 위한 클래스 </summary>
public class CameraController
{
    /// <summary> 회전 대상 카메라 </summary>
    Camera _camera;
    /// <summary> 카메라 현재 x 각도 </summary>
    float _rotationX;

    /// <summary> 프레임 당 회전 속도 </summary>
    float _rotateSpeed = 5f;

    /// <summary> 회전 대상 카메라 설정 </summary>
    public void Init(Camera camera)
    {
        _camera = camera;
    }

    /// <summary> 현재 마우스 위치에 맞게 카메라 회전 </summary>
    public void RotateCamera()
    {
        _rotationX -= Input.GetAxis("Mouse Y") * _rotateSpeed;

        //위/아래 회전은 -90 ~ 90도로 제한
        _rotationX = ClampAngle(_rotationX);

        _camera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
    }

    /// <summary> 각도 제한 </summary>
    float ClampAngle(float angle)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;

        return Mathf.Clamp(angle, -90, 90);
    }

    /// <summary> 현재 카메라 회전에 따른 이동 방향 반환 </summary>
    /// <param name="forwardWeight">앞 이동 계수</param>
    /// <param name="rightWeight">옆 이동 계수</param>
    public Vector3 GetWeightedDirection(float forwardWeight, float rightWeight)
    {
        Vector3 direction = (Forward * forwardWeight + Right * rightWeight);
        direction.y = 0;
        return direction.normalized;
    }

    Vector3 Forward => _camera.transform.forward;
    Vector3 Right => Vector3.Cross(_camera.transform.up, Forward);
}
