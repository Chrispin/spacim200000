using UnityEngine;
using System.Collections;

public class droploot : MonoBehaviour {
    public GameObject loot;
    
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void droplo(Transform tran)
    {
        Vector3 tra = tran.position;
        Quaternion q = tran.rotation;
        GameObject bull = (GameObject)Instantiate(loot, tra, q);
        

    }
}
