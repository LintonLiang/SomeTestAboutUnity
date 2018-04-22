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
            //Utility.MakeToast("΢���û���" + ssdk.GetAuthInfo(PlatformType.SinaWeibo)["userName"] + "��½�ɹ���");
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
            Debug.Log("��Ȩ�ɹ���");
            ssdk.GetAuthInfo(type);
            Utility.WriteFile(Application.persistentDataPath, "AuthResult.dat", data.toJson());
            
            Utility.WriteFile(Application.persistentDataPath, "AuthInfo.dat", ssdk.GetAuthInfo(PlatformType.SinaWeibo).toJson());
            //Utility.MakeToast("΢���û���"+ ssdk.GetAuthInfo(PlatformType.SinaWeibo)["userName"]+"��½�ɹ���");
            UnityEngine.SceneManagement.SceneManager.LoadScene(2); 
        }
        else if (state == ResponseState.Fail)
        {
            ssdk.CancelAuthorize(type);
            //Utility.MakeToast("��½ʧ�ܣ�");

        }
        else if (state == ResponseState.Cancel)
        {
            ssdk.CancelAuthorize(type);
            //Utility.MakeToast("��¼��ȡ����");
        }
    }
}
