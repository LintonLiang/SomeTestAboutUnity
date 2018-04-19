using cn.sharesdk.unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShareSDKManager : MonoBehaviour {

    private static ShareSDKManager _instance;

    public static ShareSDKManager Instance
    {
        get
        {
            return _instance;
        }

    }
    [HideInInspector]
    public ShareSDK ssdk;
    // Use this for initialization
    void Start () {


        _instance = this;
        DontDestroyOnLoad(gameObject);
        //ShareSDK类中自带有初始化的，所以这里可以不初始化，官方文档已过时
        ssdk = GetComponent<ShareSDK>();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
