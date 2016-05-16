using UnityEngine;
using System.Collections;

public class imanengine : MonoBehaviour {
    public char letter =  'w';
    public bool move = false;    
    void Start () {
        Physics2D.gravity = new Vector2(0, 0);
	}	
	void Update () {
	if(move == true)
        {
            this.GetComponent<Rigidbody2D>().AddRelativeForce(Vector2.up * 100 * Time.deltaTime);            
        }
        move = false;
	}    
    public void moveforwards(char lettr)
    {
        if (letter == lettr)
        {
            move = true;
        }
    }
}
