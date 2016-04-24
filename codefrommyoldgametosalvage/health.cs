using UnityEngine;
using System.Collections;

public class health : MonoBehaviour
{


    public GameObject go;
    public int maxhealh;
    public int currenthealth;
    public int teamset;
    //public ScriptableObject sc;
    int team;
    int healcurrent;
    // Use this for initialization
    void Start()
    {
        
        healcurrent = maxhealh;
        if(teamset != 0)
        { team = teamset;}
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        currenthealth = healcurrent;


	if(healcurrent <= 0)
        {
            
            if(go.GetComponent<droploot>() != null)
            {
                go.GetComponent<droploot>().droplo(go.GetComponent<Transform>());
            }
            
            Destroy(go);

        }
	}
    public void Damage(int damAmount, int teamsender)
    {
        if (teamsender != team)
        {
            healcurrent -= damAmount;
        }
    }
    public void setteam(int maketeam)
    {
        team = maketeam;
    }
    public int getteam()
    {
        return team;
    }
    public void addhealth(int amount)
    {
        healcurrent += amount;
        if(healcurrent > maxhealh)
        {

            healcurrent = maxhealh;
        }


    }
}
