using UnityEngine;
using System.Collections;

public class loadlevel : MonoBehaviour {

    
	// Use this for initialization
	void Start () {
        //Application.LoadLevel(index);
        //UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    public void loadscene(string scenee)
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(scenee);

    }
}
