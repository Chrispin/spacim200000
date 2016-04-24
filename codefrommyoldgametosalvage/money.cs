using UnityEngine;
using System.Collections;

public class money : MonoBehaviour {

    public GameObject go;
    public int moneyvalue;
    public Sprite tier1;
    public Sprite tier2;
    // Use this for initialization
    void Start()
    {
        // go.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, -600));
    }

    // Update is called once per frame
    void Update()
    {
        //go.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0, 10));
        if (moneyvalue <= 0)
        {
            Destroy(go);

        }
        if (moneyvalue < 5)
        {
            //go.GetComponent<Sprite>().
            go.GetComponent<SpriteRenderer>().sprite = tier1;

        }
        if (moneyvalue >= 6)
        {
            //go.GetComponent<Sprite>().
            go.GetComponent<SpriteRenderer>().sprite = tier2;

        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<player>().enabled == true)
        {
            int collcurrmon = coll.gameObject.GetComponent<player>().money;
            int collmaxmon = coll.gameObject.GetComponent<player>().maxmoney;
            int diff = collmaxmon - collcurrmon;
            if (diff >= moneyvalue)
            {
                diff = moneyvalue;
            }
            coll.gameObject.GetComponent<player>().addmoney(diff);
            moneyvalue -= diff;

            //coll.gameObject.GetComponent<health>().

            //retardedlel();
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.GetComponent<money>().enabled == true)
        {
            moneyvalue += coll.gameObject.GetComponent<money>().moneyvalue;
            coll.gameObject.GetComponent<money>().moneyvalue = 0;
        }
    }
}
