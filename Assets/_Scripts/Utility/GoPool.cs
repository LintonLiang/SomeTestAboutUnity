using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoPool : MonoBehaviour {
    public int poolCount = 10;
    public GameObject poolMemberOriginal;
    public float delayTime = 3f;
    private List<GameObject> poolList;
    
	// Use this for initialization
	void Start () {
        poolList = new List<GameObject>();
        InitPool();

    }

    void InitPool()
    {
        for (int i = 0; i < poolCount; i++)
        {
            GameObject go = Instantiate(poolMemberOriginal);
            poolList.Add(go);
            go.SetActive(false);
            go.transform.parent = this.transform;
        }
    }
    public GameObject GetGo()
    {
        foreach (GameObject go in poolList)
        {
            if (go.activeInHierarchy == false)
            {
                go.SetActive(true);
                StartCoroutine(DestoryGo(go));
                return go;
            }
                
        }
        GameObject otherGO = Instantiate(poolMemberOriginal);
        otherGO.SetActive(true);
        otherGO.transform.parent = this.transform;
        Destroy(otherGO, delayTime);
        return otherGO;
    }
    IEnumerator DestoryGo(GameObject go)
    {
        yield return new WaitForSeconds(delayTime);
        go.SetActive(false);
    }
}
