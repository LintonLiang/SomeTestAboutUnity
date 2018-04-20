using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UitilityTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log(Utility.ReadFile(@"C:\Users\Administrator\Desktop", "siki.txt"));
        Utility.WriteFile(@"C:\Users\Administrator\Desktop", "siki.txt", "测试测试自己写的写入脚本");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
