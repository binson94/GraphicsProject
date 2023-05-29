/******
도움말 용 스크린샷
 ******/
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    static int screenShot;

    void Start()
    {
        screenShot = PlayerPrefs.GetInt("screenshot", 0);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ScreenCapture.CaptureScreenshot($"screenshot{screenShot}.png");
            PlayerPrefs.SetInt("screenshot", ++screenShot);
        }
    }
}
