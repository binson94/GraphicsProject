/******
작성자 : 
박스 클래스
 ******/

using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    Animator _boxAnimator;

    /// <summary> 열쇠 보유 여부 </summary>
    bool _hasKey = false;
    /// <summary> 열쇠 보유 여부 </summary>
    public bool HasKey
    {
        get => _hasKey; 
        set
        {
            _hasKey = value;
            _treasureHide.SetActive(!value);
        }
    }
    /// <summary> 상자 열림 여부 </summary>
    public bool Opened { get; set; }

    [SerializeField]
    GameObject _treasureHide;

    private void Awake()
    {
        Opened = false;
        GameManager.Instance.AddBox(this);
    }

    /// <summary> 상자 열기 </summary>
    public void Open()
    {
        if(Opened) return;
        Opened = true;

        _boxAnimator.Play("Box_Open");

        GameManager.Instance.OnBoxOpened(transform.position, HasKey);
    }
}
