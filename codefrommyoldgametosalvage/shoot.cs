using UnityEngine;
using System.Collections;

public class shoot : MonoBehaviour
{
    public GameObject go;
    public GameObject bullett;
    int team;
    public bool local = false;
    public bool shotdirty = false;
    public int shotcount;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && local)
        {
            Fire();
            //bull.GetComponent<health>().setteam(team);
        }

    }

    public void Fire()
    {
        Vector3 tra = go.GetComponent<Transform>().transform.position;
        Quaternion q = go.GetComponent<Transform>().rotation;
        GameObject bull = (GameObject)Instantiate(bullett, tra, q);
        Physics2D.IgnoreCollision(bull.GetComponent<BoxCollider2D>(), go.GetComponent<BoxCollider2D>());
        shotdirty = true;
    }

    public void setteam(int maketeam)
    {
        team = maketeam;
    }
}
