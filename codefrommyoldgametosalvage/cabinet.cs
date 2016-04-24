using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cabinet : MonoBehaviour
{
    public GameObject go;
    public List<GameObject> drop;
    
    bool dropped;
    // Use this for initialization
    void Start()
    {
        dropped = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (dropped)
        {
            dropped = false;
            Vector3 t = go.GetComponent<Transform>().position;
            Quaternion q = go.GetComponent<Transform>().rotation;

            GameObject er = Instantiate(drop[UnityEngine.Random.Range(0,drop.Count)], t, q) as GameObject;
            er.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -600));
            Physics2D.IgnoreCollision(er.GetComponent<BoxCollider2D>(), go.GetComponent<BoxCollider2D>());            
        }
    }
}
