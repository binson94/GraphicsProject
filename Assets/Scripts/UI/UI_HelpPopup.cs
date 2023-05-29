/******
작성자 : 이우열
도움말 UI
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

    [SerializeField]
    Sprite[] _tutorialSprites;
    string[] _tutorialTxts;

    /// <summary> 현재 도움말 페이지 </summary>
    int _currPage = 0;
    /// <summary> 도움말 총 페이지 수 </summary>
    [SerializeField]
    int _maxPage;

    private void Awake()
    {
        LoadData();
    }

    /// <summary> 도움말 텍스트 불러오기 </summary>
    void LoadData()
    {
        TextAsset txtAsset = Resources.Load<TextAsset>("Tutorials/tutoTxts");

        _tutorialTxts = txtAsset.text.Split('%');

        foreach(string s in _tutorialTxts)
            s.Replace("\\\\", "\\");
        _maxPage = _tutorialTxts.Length - 1;
    }

    /// <summary> 도움말 창 열기 </summary>
    public void Open()
    {
        OpenPage(0);
    }

    /// <summary> 도움말 이미지와 텍스트 업데이트 </summary>
    void OpenPage(int page)
    {
        _currPage = page;
        _helpImage.sprite = _tutorialSprites[page];
        _helpText.text = _tutorialTxts[page];

        _prevBtn.SetActive(page > 0);
        _nextBtn.SetActive(page < _maxPage);
    }

    /// <summary> 이전, 다음 버튼 -> 도움말 페이지 넘기기 </summary>
    public void Btn_MovePage(int diff)
    {
        int nextPage = Mathf.Clamp(_currPage + diff, 0, _maxPage);
        OpenPage(nextPage);
    }

    /// <summary> 도움말 창 닫기 </summary>
    public void Btn_Close()
    {
        gameObject.SetActive(false);
    }
}
