using cn.sharesdk.unity3d;
using System.Collections;
using cn.SMSSDK.Unity;
using UnityEngine;

public class LoginManager : MonoBehaviour,SMSSDKHandler {
    ShareSDK ssdk;
    SMSSDK smssdk;
	// Use this for initialization
	void Start () {
        
        ssdk = ShareSDKManager.Instance.ssdk;
        smssdk = ShareSDKManager.Instance.smssdk;

        ssdk.authHandler = OnAuthResultHandler;
        smssdk.setHandler(this);

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
    public void OnSmsLoginButtonClick()
    {
        smssdk.showRegisterPage(CodeType.TextCode);
    }
    void OnAuthResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable data)
    {
        if (state == ResponseState.Success)
        {
            Debug.Log("授权成功！");
            ssdk.GetAuthInfo(type);
            Utility.WriteFile(Application.persistentDataPath, "AuthResult.dat", data.toJson());
            
            Utility.WriteFile(Application.persistentDataPath, "AuthInfo.dat", ssdk.GetAuthInfo(PlatformType.SinaWeibo).toJson());
            ShareSDKManager.Instance.userPlat = PlatformType.SinaWeibo;
            //ShareSDKManager.Instance.userID = ssdk.GetAuthInfo(PlatformType.SinaWeibo)["userID"].ToString();
            Utility.MakeToast("微博用户："+ ssdk.GetAuthInfo(PlatformType.SinaWeibo)["userName"]+"登陆成功！");
            UnityEngine.SceneManagement.SceneManager.LoadScene(2); 
        }
        else if (state == ResponseState.Fail)
        {
            ssdk.CancelAuthorize(type);
            Utility.MakeToast("登陆失败！");

        }
        else if (state == ResponseState.Cancel)
        {
            ssdk.CancelAuthorize(type);
            Utility.MakeToast("登录被取消！");
        }
    }

    public void onComplete(int action, object resp)
    {
        ActionType act = (ActionType)action;
        if (act == ActionType.CommitCode)
        {
            ShareSDKManager.Instance.userPlat = PlatformType.SMS;
            ShareSDKManager.Instance.userID = ((string)resp).hashtableFromJson()["phone"].ToString();
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
    }

    public void onError(int action, object resp)
    {
        Utility.MakeToast("短信登录失败");
    }
}
