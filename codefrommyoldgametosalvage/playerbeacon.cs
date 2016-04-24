using UnityEngine;
using System.Collections;

public class playerbeacon : MonoBehaviour {
    public bool MovementDirty;
    private float lastUpdate;
    private float wait = 0.05F;

    // Use this for initialization
    void Start () {
        MovementDirty = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Time.time - lastUpdate >= wait)
        {
MovementDirty = true;
            lastUpdate = Time.time;
        }
    }
}
