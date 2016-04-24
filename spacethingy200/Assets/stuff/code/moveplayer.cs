using UnityEngine;
using System.Collections;

public class moveplayer : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {

        if (!this.GetComponentInParent<moveengines>().isdriving)
        {            
            if (Input.GetKey(KeyCode.W))
            {
                this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.left * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.right * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.down * Time.deltaTime);
            }
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 5.23f;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
            this.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }
        else
        {
            this.GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 0);
        }
    }
}
