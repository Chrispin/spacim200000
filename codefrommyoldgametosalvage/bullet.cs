using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour
{

    public GameObject go;
    public int speed;
    public int damage;
    public int team;
    public float wait;
    Collider2D cl2d;
    float lastUpdate;
    // Use this for initialization
    void Start()
    {
        cl2d = go.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        go.GetComponent<Transform>().Translate(Vector3.up * speed * Time.deltaTime);
        Destroy(go, wait);

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Physics2D.IgnoreCollision(coll.collider, cl2d);
        if (coll.gameObject.tag == "damageable")
        {
            if (coll.gameObject.GetComponent<health>().getteam() != 6)
            {
                coll.gameObject.GetComponent<health>().Damage(damage, team);
                Destroy(go);
            }
        }
        // Destroy(coll.gameObject);
        // this.GetComponent<health>().heal -= damage;

    }
}
