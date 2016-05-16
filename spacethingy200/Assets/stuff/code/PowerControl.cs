using UnityEngine;
using System.Collections;

public class PowerControl : MonoBehaviour {
    GameObject engineer;
    public bool isengineer;
       
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && isengineer)
        {
            isengineer = false;
        }
        if (isengineer)
        {
            engineer.GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 0);
            engineer.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
            foreach (imanengine t in this.GetComponentsInChildren<imanengine>())
            {

            }
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            engineer = coll.gameObject;
            isengineer = true;
            engineer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        }
    }
}
