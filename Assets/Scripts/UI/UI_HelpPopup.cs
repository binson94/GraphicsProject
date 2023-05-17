/******
작성자 : 이우열
작성 일자 : 23.05.17
 ******/
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HelpPopup : MonoBehaviour
{
    /// <summary> 도움말 이미지 </summary>
    [SerializeField]
    Image _helpImage;
    /// <summary> 도움말 텍스트 </summary>
    [SerializeField]
    TMP_Text _helpText;

    /// <summary> 이전 버튼 </summary>
    [SerializeField]
    GameObject _prevBtn;
    /// <summary> 다음 버튼 </summary>
    [SerializeField]
    GameObject _nextBtn;

    /// <summary> 현재 도움말 페이지 </summary>
    int _currPage = 0;
    /// <summary> 도움말 총 페이지 수 </summary>
    const int MAXPAGE = 2;

    /// <summary> 도움말 창 열기 </summary>
    public void Open()
    {
        OpenPage(0);
    }

    /// <summary> 도움말 이미지와 텍스트 업데이트 </summary>
    void OpenPage(int page)
    {
        _currPage = page;
        _helpText.text = $"page {page}";

        _prevBtn.SetActive(page > 0);
        _nextBtn.SetActive(page < MAXPAGE);
    }

    /// <summary> 이전, 다음 버튼 -> 도움말 페이지 넘기기 </summary>
    public void Btn_MovePage(int diff)
    {
        int nextPage = Mathf.Clamp(_currPage + diff, 0, MAXPAGE);
        OpenPage(nextPage);
    }

    /// <summary> 도움말 창 닫기 </summary>
    public void Btn_Close()
    {
        gameObject.SetActive(false);
    }
}
