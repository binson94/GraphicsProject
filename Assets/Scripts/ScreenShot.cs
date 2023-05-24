using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class ScreenShot : MonoBehaviour
{
    static int screenShot;

    void Start()
    {
        screenShot = PlayerPrefs.GetInt("screenshot", 0);

        ScreenCapture.CaptureScreenshot("map.png");
        Debug.Log(Application.dataPath);
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
