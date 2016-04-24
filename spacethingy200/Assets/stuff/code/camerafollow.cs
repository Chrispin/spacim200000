using UnityEngine;
using System.Collections;

public class camerafollow : MonoBehaviour
{
    public GameObject follow;
    private float lastUpdate;
    private float wait = 2;
    void LateUpdate()
    {
        if (follow != null)
        {
            Vector3 por = new Vector3(follow.GetComponent<Transform>().position.x, follow.GetComponent<Transform>().position.y, -30);
            this.GetComponent<Transform>().position = por;
        }
        else
        {
            if (Time.time - lastUpdate >= wait)
            {
                Debug.Log("camera has no follower");
                lastUpdate = Time.time;
            }            
        }
    }
}
