using cn.sharesdk.unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour {

    public Image userIcon;
    public Text userID;
    public Text userName;

    
    ShareSDK ssdk;
	// Use this for initialization
	void Start () {
        ssdk = ShareSDKManager.Instance.ssdk;
        ssdk.showUserHandler = OnGetUserInfoResultHandler;

        if (ShareSDKManager.Instance.userPlat ==PlatformType.SinaWeibo)
        {
            Hashtable authInfo = Utility.ReadFile(Application.persistentDataPath, "AuthInfo.dat").hashtableFromJson();

            StartCoroutine(LoadUserIcon(authInfo["userIcon"].ToString()));
            userID.text = authInfo["userID"].ToString();
            userName.text = authInfo["userName"].ToString();
        }
        if (ShareSDKManager.Instance.userPlat ==PlatformType.SMS)
        {
            userName.text = ShareSDKManager.Instance.userID;
        }

        

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator LoadUserIcon(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone&&www.error == null)
        {
            Texture2D texture = www.texture;
            userIcon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            Debug.Log("图片已经下载了");
        }
    }
    
    public void OnEnterButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
    public void OnDetailButtonClick()
    {
        if (ShareSDKManager.Instance.userPlat == PlatformType.SinaWeibo)
        {
            ssdk.GetUserInfo(PlatformType.SinaWeibo);
        }
        if (ShareSDKManager.Instance.userPlat == PlatformType.SMS)
        {
            Utility.MakeToast("手机注册用户无法完成此操作");
        }
        
    }
    public void OnSignOutButtonClick()
    {
        if (ShareSDKManager.Instance.userPlat == PlatformType.SinaWeibo)
        {
            ssdk.CancelAuthorize(PlatformType.SinaWeibo);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    void OnGetUserInfoResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable data)
    {
        if (state == ResponseState.Success)
        {
            Utility.WriteFile(Application.persistentDataPath, "UserInfo.dat", data.toJson());

            Utility.MakeToast("您的位置：" + Utility.UnicodeToString(data["location"].ToString()));
        }
        else if (state == ResponseState.Fail)
        {
            ssdk.CancelAuthorize(type);
            Utility.MakeToast("获取用户详细信息失败！");

        }
        else if (state == ResponseState.Cancel)
        {
            ssdk.CancelAuthorize(type);
            Utility.MakeToast("获取用户详细信息被取消！");
        }
    }
    
}
