using cn.sharesdk.unity3d;
using UnityEngine;
using cn.SMSSDK.Unity;

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
    public PlatformType userPlat = PlatformType.Unknown;
    [HideInInspector]
    public string userID = "";
    [HideInInspector]
    public ShareSDK ssdk;
    [HideInInspector]
    public SMSSDK smssdk;
    
    // Use this for initialization
    void Start () {


        _instance = this;
        DontDestroyOnLoad(gameObject);
        //ShareSDK类中自带有初始化的，所以这里可以不初始化，官方文档已过时
        ssdk = GetComponent<ShareSDK>();
        Debug.Log("ssdk已经初始化完成");
        //初始化SMSSDK,必须自己写初始化
        try
        {
            smssdk =gameObject.GetComponent<SMSSDK>();
            smssdk.init("25510aaf268e3", "34120cf93109bcc76ec1c553337eda4c", true);
        }
        catch (System.Exception e)
        {

            Debug.Log("SMSSDK注册出错，错误原因："+e);
        }
        

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
