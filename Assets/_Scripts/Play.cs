using cn.sharesdk.unity3d;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Play : MonoBehaviour {

    public Text resultText;
    

    ShareSDK ssdk;

    int friendBuff;
	// Use this for initialization
	void Start () {

        ssdk = ShareSDKManager.Instance.ssdk;
        ssdk.getFriendsHandler = OnGetFriendResuleHandler;
        ssdk.shareHandler = OnShareResultHandler;
        


    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnShareButtonClick()
    {
        ScreenCapture.CaptureScreenshot("Screenshot.png");
        ShareContent content = new ShareContent();

        //公共信息设置
        content.SetText(resultText.text);
        content.SetImagePath(Application.persistentDataPath+ "/Screenshot.png");
        content.SetTitle("我的share");
        content.SetTitleUrl("https://github.com/LintonLiang");
        content.SetSite("Lintonliang");
        content.SetSiteUrl("https://github.com/LintonLiang");
        content.SetUrl("https://github.com/LintonLiang");
        content.SetUrlDescription("我的Github主页");
        content.SetShareType(ContentType.Image);

        //各个平台特例的设置
        //新建一个content，添加上特殊的信息，对公共信息进行补充和替换，相当于C#中的继承
        ShareContent sinaContent = new ShareContent();
        sinaContent.SetText(resultText.text + "\n 通过新浪微博分享");
        content.SetShareContentCustomize(PlatformType.SinaWeibo, sinaContent);


        
        ssdk.ShowPlatformList(null, content, 100, 100);
    }
    public void OnSignOutButtonClick()
    {
        if (ShareSDKManager.Instance.userPlat == PlatformType.SinaWeibo)
        {
            ssdk.CancelAuthorize(PlatformType.SinaWeibo);
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    public void OnPlayButtonClick()
    {
       
        
        if (ShareSDKManager.Instance.userPlat == PlatformType.SinaWeibo)
        {
            resultText.text = "恭喜获得了\n好友加成：" + friendBuff;
        }
        if (ShareSDKManager.Instance.userPlat == PlatformType.SMS)
        {
            
            resultText.text = "恭喜用户：" + ShareSDKManager.Instance.userID+"\n获得了神器屠龙宝刀";
        }
    }
    public void OnFriendsButtonClick()
    {
        if (ShareSDKManager.Instance.userPlat == PlatformType.SinaWeibo)
        {
            ssdk.GetFriendList(PlatformType.SinaWeibo, 15, 0);
        }
        if (ShareSDKManager.Instance.userPlat == PlatformType.SMS)
        {
            Utility.MakeToast("手机注册用户无法完成此操作");
        }
       
    }
    void OnGetFriendResuleHandler(int reqID, ResponseState state, PlatformType type, Hashtable data)
    {
        if (state == ResponseState.Success)
        {
            Utility.WriteFile(Application.persistentDataPath, "FriendList.dat", data.toJson());
            friendBuff = int.Parse( data["total_number"].ToString());
            Utility.MakeToast("获取好友加成："+friendBuff);
        }
        else if (state == ResponseState.Fail)
        {
            ssdk.CancelAuthorize(type);
            Utility.MakeToast("获取用户好友失败！");

        }
        else if (state == ResponseState.Cancel)
        {
            ssdk.CancelAuthorize(type);
            Utility.MakeToast("获取好友被取消！");
        }
    }
    void OnShareResultHandler(int reqID, ResponseState state, PlatformType type, Hashtable data)
    {
        if (state == ResponseState.Success)
        {
            Utility.MakeToast("分享成功！");
        }
        else if (state == ResponseState.Fail)
        {
            
            Utility.MakeToast("分享失败！");

        }
        else if (state == ResponseState.Cancel)
        {
            
            Utility.MakeToast("分享被取消！");
        }
    }
}
