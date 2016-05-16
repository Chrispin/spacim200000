using UnityEngine;
using System.Collections;

public class PilotControl : MonoBehaviour {
    GameObject driver;
    public bool isdriver;
	
	void Start ()
    {	

	}

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && isdriver)
        {
            isdriver = false;
        }
        if(isdriver)
        {
            driver.GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 0);
            driver.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
            foreach (imanengine t in this.GetComponentsInChildren<imanengine>())
            {
                if (Input.GetKey(KeyCode.W))
                {
                    t.moveforwards('w');
                }
                if (Input.GetKey(KeyCode.S))
                {
                    t.moveforwards('s');
                }
                if (Input.GetKey(KeyCode.A))
                {
                    t.moveforwards('a');
                }
                if (Input.GetKey(KeyCode.D))
                {
                    t.moveforwards('d');
                }                
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            driver = coll.gameObject;
            isdriver = true;
            driver.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
        }
    }
}
