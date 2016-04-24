using UnityEngine;
using System.Collections;
using System;

public class zombie : MonoBehaviour
{
    public GameObject go;
    GameObject player;
    public float speed;
    float speedreal;
    public int damage;
    public float wait;
    float lastUpdate;
    public float maxdis = 3;
    public int wandeg = 20;
    public float wait2;
    float lastUpdate2;
    public int team;
    //public GameObject droploot;

    float finalan;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        finalan = 0;
        go.GetComponent<health>().setteam(team);
        speedreal = speed;
    }


    void Update()
    {
        go.GetComponent<Transform>().Translate(Vector3.up * speedreal * Time.deltaTime);
        try
        {
            Vector3 mousePos = player.GetComponent<Transform>().position;
            mousePos.z = 5.23f;
            Vector3 objectPos = GetComponent<Transform>().position;
            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;
            float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;//player angle

            /*
            int x = Getplayerpos();
            followplayer();
            bool fail = true;
            if(fail)
            {
                zombiexcecute("plan B");
            }
            */

            float dis = Vector2.Distance(go.GetComponent<Transform>().position, player.GetComponent<Transform>().position);



            if (Time.time - lastUpdate2 >= wait2)
            {
                finalan = RonsorWander(dis, maxdis, wandeg, angle - 90);

                lastUpdate2 = Time.time;
            }


            go.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, finalan));
        }
        catch { }
    }


    float RonsorWander(float distance, float maxdistance, int wandeg, float playerangle)
    {
        if (distance <= maxdistance)
        {
            return UnityEngine.Random.Range(-wandeg, wandeg) + playerangle;
        }
        else
        {
            return UnityEngine.Random.Range(0, 360);

        }

    }

    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "damageable")
        {
            if (Time.time - lastUpdate >= wait)
            {
                coll.gameObject.GetComponent<health>().Damage(damage, team);
                lastUpdate = Time.time;
            }
        }


        // Destroy(coll.gameObject);
        // this.GetComponent<health>().heal -= damage;

    }
}

