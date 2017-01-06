using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_obj : MonoBehaviour {
    public GameObject Explosion;
	// Use this for initialization
	void Start () {
		
	}
    
    private void OnDestroy()
    {
        if (Explosion != null)
        {
            GameObject obj = (GameObject)Instantiate(Explosion);
            obj.transform.position = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player_Bullet":
                Destroy(collision.gameObject);
                Destroy(gameObject);

                break;
            case "Player":
                Destroy(gameObject);
                break;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
