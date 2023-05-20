/******
작성자 : 
출구 클래스
 ******/

using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    bool _opened = false;

    /// <summary> 출구 열기 </summary>
    public void Open()
    {
        if (GameManager.Instance.CanOpenDoor == false)
            return;

        if(_opened) return;
        _opened = true;

        GameManager.Instance.GameWin();

        StartCoroutine(OpenCoroutine());
    }

    IEnumerator OpenCoroutine()
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.x = 0;

        while(rotation.x < 90)
        {
            rotation.x += 1f;
            transform.rotation = Quaternion.Euler(rotation);

            yield return null;
        }

        rotation.x = 90;
        transform.rotation = Quaternion.Euler(rotation);

        GameManager.Instance.ShowWinUI();
    }
}
