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
        Hashtable authInfo = Utility.ReadFile(Application.persistentDataPath, "AuthInfo.dat").hashtableFromJson();

        StartCoroutine(LoadUserIcon(authInfo["userIcon"].ToString()));
        userID.text = authInfo["userID"].ToString();
        userName.text = authInfo["userName"].ToString();

        

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
    

}
