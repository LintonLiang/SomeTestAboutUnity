using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class AndroidJavaProxyTest : MonoBehaviour {
    DateTime dateNow = DateTime.Now;
    static Text text;

    class DateCallBack:AndroidJavaProxy
    {
        
        public DateCallBack():base("android.app.DatePickerDialog$OnDateSetListener") { }

        void onDateSet(AndroidJavaObject view ,int year,int month,int dayOfMonth)
        {
            AndroidJavaProxyTest.text.text = year + "/" + (month+1) + "/" + dayOfMonth;
        }
    }
	// Use this for initialization
	void Start () {
        text = GameObject.Find("Text").GetComponent<Text>();
        AndroidJavaObject currentActivity = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity");
        currentActivity.Call("runOnUiThread", new AndroidJavaRunnable(() =>
        {
            new AndroidJavaObject("android.app.DatePickerDialog", currentActivity,new DateCallBack(), dateNow.Year, dateNow.Month-1, dateNow.Day).Call("show");
        }));
		
	}
	
	
}
