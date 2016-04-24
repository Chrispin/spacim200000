using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class player : MonoBehaviour
{

    public GameObject go;
    public int money;
    public int maxmoney;
    //Rigidbody2D rb2d;
    public int speed;
    GameObject[] roofs;
    public int team;
    bool hold;
    public float wait;
    float lastUpdate;
    public Text counttext;
    public Text montext;

    // Use this for initialization
    void Start()
    {
        
        go.GetComponent<shoot>().setteam(team);
        go.GetComponent<health>().setteam(team);
        hold = false;
        Physics2D.gravity = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        go.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));

        counttext.text = go.GetComponent<health>().currenthealth.ToString();
        montext.text = money.ToString();






        if (Input.GetKey(KeyCode.W))
        {
            go.GetComponent<Rigidbody2D>().transform.position += Vector3.up * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            go.GetComponent<Rigidbody2D>().transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            go.GetComponent<Rigidbody2D>().transform.position += Vector3.down * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            go.GetComponent<Rigidbody2D>().transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && hold)
        {
            if (Time.time - lastUpdate >= wait)
            {
                Destroy(go.GetComponent<FixedJoint2D>());
                hold = false;
                lastUpdate = Time.time;
            }
        }
        //GameObject.get
        //i want to get all roofs and make them transparent


    }
    public void seteverything(GameObject go1, int moneycount, int maxmoney1, int speed1, float wait1, Text counttestt, Text montext2)
    {
        go = go1;
        money = moneycount;
        maxmoney = maxmoney1;
        speed = speed1;
        team = 1;
        wait = wait1;
        counttext = counttestt;
        montext = montext2;
        
    } 

    void OnCollisionStay2D(Collision2D coll)
    {
        if (Time.time - lastUpdate >= wait)
        {
            if (Input.GetKey(KeyCode.Space) && !hold)
            {
                go.AddComponent<FixedJoint2D>();
                go.GetComponent<FixedJoint2D>().connectedBody = coll.rigidbody;
                go.GetComponent<FixedJoint2D>().anchor = new Vector2(0, 1);
                go.GetComponent<FixedJoint2D>().enableCollision = false;
                hold = true;
                lastUpdate = Time.time;
            }
        }
    }
    public void addmoney(int coun)
    {
        money += coun;


    }
}
