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
            Utility.WriteFile(Application.persistentDataPath, "AuthInfo.dat", ssdk.GetAuthInfo(PlatformType.SinaWeibo).toJson());
            //Utility.MakeToast("微博用户：" + ssdk.GetAuthInfo(PlatformType.SinaWeibo)["userName"] + "登陆成功！");
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
            Debug.Log("授权成功！");
            ssdk.GetAuthInfo(type);
            Utility.WriteFile(Application.persistentDataPath, "AuthResult.dat", data.toJson());
            
            Utility.WriteFile(Application.persistentDataPath, "AuthInfo.dat", ssdk.GetAuthInfo(PlatformType.SinaWeibo).toJson());
            //Utility.MakeToast("微博用户："+ ssdk.GetAuthInfo(PlatformType.SinaWeibo)["userName"]+"登陆成功！");
            UnityEngine.SceneManagement.SceneManager.LoadScene(2); 
        }
        else if (state == ResponseState.Fail)
        {
            ssdk.CancelAuthorize(type);
            //Utility.MakeToast("登陆失败！");

        }
        else if (state == ResponseState.Cancel)
        {
            ssdk.CancelAuthorize(type);
            //Utility.MakeToast("登录被取消！");
        }
    }
}
