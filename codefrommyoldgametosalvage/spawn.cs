using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class spawn : MonoBehaviour
{

    // Use this for initialization
    public GameObject go;
    public List<GameObject> zomToSpawn;
    public double x;
    public double y;
    public float wait;
    public int team;
    float lastUpdate;
	void Start ()
    {
        //zomToSpawn = new List<GameObject>();
        go.GetComponent<health>().setteam(team);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time - lastUpdate >= wait)
        {
            GameObject er = Instantiate(zomToSpawn[UnityEngine.Random.Range(0,zomToSpawn.Count)], go.GetComponent<Transform>().position, go.GetComponent<Transform>().rotation) as GameObject;
            Physics2D.IgnoreCollision(er.GetComponent<BoxCollider2D>(), go.GetComponent<BoxCollider2D>());          
            lastUpdate = Time.time;
        }

    }
}
