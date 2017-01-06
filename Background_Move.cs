using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Move : MonoBehaviour {
    public float Speed;
    public float Distance;
    public float Limit;
    
    // Use this for initialization
    void Start () {

	}
	
    private void Location()
    {
        transform.GetChild(0).transform.position += Vector3.back * Time.deltaTime * Speed;
        transform.GetChild(1).transform.position += Vector3.back * Time.deltaTime * Speed;
        
        if (transform.GetChild(0).transform.position.z <= -Limit)
        {
            Vector3 vec = transform.GetChild(1).transform.position;
            transform.GetChild(0).transform.position = new Vector3(vec.x, vec.y, vec.z + Distance);
        }

        if (transform.GetChild(1).transform.position.z <= -Limit)
        {
            Vector3 vec = transform.GetChild(0).transform.position;
            transform.GetChild(1).transform.position = new Vector3(vec.x, vec.y, vec.z + Distance);
        }
    }

	// Update is called once per frame
	void Update () {
        Location();
	}
}
