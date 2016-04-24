using UnityEngine;
using System.Collections;
using System;

public class medkit : MonoBehaviour
{
    public GameObject go;
    public int hpworth;
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
        if(hpworth <= 0)
        {
            Destroy(go);

        }
        if (hpworth < 60)
        {
            //go.GetComponent<Sprite>().
            go.GetComponent<SpriteRenderer>().sprite = tier1;

        }
        if (hpworth >= 60)
        {
            //go.GetComponent<Sprite>().
            go.GetComponent<SpriteRenderer>().sprite = tier2;
            
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {

        int collcurrhp = coll.gameObject.GetComponent<health>().currenthealth;
        int collmaxhp = coll.gameObject.GetComponent<health>().maxhealh;
        int diff = collmaxhp - collcurrhp;
        if (diff >= hpworth)
        {
            diff = hpworth;
        }
        coll.gameObject.GetComponent<health>().addhealth(diff);
        hpworth -= diff;

        //coll.gameObject.GetComponent<health>().

        //retardedlel();

    }
        void OnCollisionStay2D(Collision2D coll)
    {
        if(coll.gameObject.GetComponent<medkit>().enabled == true)
        {
            hpworth += coll.gameObject.GetComponent<medkit>().hpworth;
            coll.gameObject.GetComponent<medkit>().hpworth = 0;
        }
        
    }


}
