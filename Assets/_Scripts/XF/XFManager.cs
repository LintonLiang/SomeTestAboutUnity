using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XFManager : MonoBehaviour {

    public Text resultText;
    private AndroidJavaObject currentActivity;
    private void Start()
    {
        currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
    }
    public void OnResult(string s)
    {
        resultText.text = s;
    }
    public void OnBtnClick()
    {
        currentActivity.Call("startListen");
    }
}
