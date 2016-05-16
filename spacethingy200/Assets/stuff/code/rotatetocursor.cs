using UnityEngine;
using System.Collections;

public class rotatetocursor : MonoBehaviour {

    public GameObject projectile;

    // Update is called once per frame
    void Update() {
        
        
    }

    public void rotateToCursor(Vector3 mousePos, Vector3 objectPos)
    {
        mousePos.z = 5.23f;
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        this.GetComponent<Transform>().rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }

    public void Fire()
    {
        Vector3 tra = this.GetComponent<Transform>().transform.position;
        Quaternion q = this.GetComponent<Transform>().rotation;
        GameObject bull = (GameObject)Instantiate(projectile, tra, q);
        Physics2D.IgnoreCollision(bull.GetComponent<BoxCollider2D>(), this.GetComponent<BoxCollider2D>());        
    }
}
