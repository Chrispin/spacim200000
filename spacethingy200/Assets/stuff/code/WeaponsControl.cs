using UnityEngine;
using System.Collections;

public class WeaponsControl : MonoBehaviour {
    GameObject gunner;
    public bool isgunner;

    void Start()
    {

    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && isgunner)
        {
            isgunner = false;
        }
        
        if (isgunner)
        {
            gunner.GetComponent<Transform>().localRotation = new Quaternion(0, 0, 0, 0);
            gunner.GetComponent<Transform>().position = this.GetComponent<Transform>().position;
            Vector3 mousePos = Input.mousePosition;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
            foreach (rotatetocursor t in this.GetComponentsInChildren<rotatetocursor>())
            {
                t.rotateToCursor(mousePos, objectPos);

                if (Input.GetMouseButtonDown(0))
                {
                    t.Fire();
                }
            }

        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            gunner = coll.gameObject;
            isgunner = true;
            gunner.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
