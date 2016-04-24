using UnityEngine;
using System.Collections;

public class Shop : MonoBehaviour {

    public GameObject go;
    public GameObject spawnin;
    public int cost;
    public int storagecount;
    public float wait2;
    float lastUpdate2;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (Time.time - lastUpdate2 >= wait2)
        {


            lastUpdate2 = Time.time;

            if (coll.gameObject.GetComponent<player>().money >= cost)
            {
                coll.gameObject.GetComponent<player>().money -= cost;
                Vector3 t = go.GetComponent<Transform>().position;
                Quaternion q = go.GetComponent<Transform>().rotation;
                GameObject er = Instantiate(spawnin, t, q) as GameObject;
                er.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -600));
                Physics2D.IgnoreCollision(er.GetComponent<BoxCollider2D>(), go.GetComponent<BoxCollider2D>());
            }
        }
    }
}
