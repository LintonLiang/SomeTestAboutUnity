using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    GoPool goPool;
    // Use this for initialization
    void Start () {
        //Debug.Log(Utility.ReadFile(@"C:\Users\Administrator\Desktop", "siki.txt"));
        //Utility.WriteFile(@"C:\Users\Administrator\Desktop", "siki.txt", "测试测试自己写的写入脚本");

        goPool = GetComponent<GoPool>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject go = goPool.GetGo();
            go.transform.position = transform.position;
            go.GetComponent<Rigidbody>().velocity = transform.forward * 50f;
        }
		
	}

    
}
