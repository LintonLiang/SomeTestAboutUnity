using cn.sharesdk.unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginManager : MonoBehaviour {
    ShareSDK ssdk;
	// Use this for initialization
	void Start () {
        
        ssdk = ShareSDKManager.Instance.ssdk;
        ssdk.authHandler = OnAuthResultHandler;


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnSinaLoginBtnClick()
    {
        
        if (ssdk.IsAuthorized(PlatformType.SinaWeibo))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else
        {
            ssdk.Authorize(PlatformType.SinaWeibo);
        }
    }

    void OnAuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable data)
    {
        if (state == ResponseState.Success)
        {

        }
        else if (state == ResponseState.Fail)
        {

        }
        else if (state == ResponseState.Cancel)
        {

        }
    }
}
