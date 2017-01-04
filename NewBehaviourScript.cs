using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public GameObject My;
    private bool jump = false;
	// Use this for initialization
	void Start () {
        print("Start");
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(Vector3.up * 3);
        //transform.localScale += Vector3.one * 0.01f;
        //transform.position += Vector3.left * 0.01f;
        Vector3 location_my = My.transform.position;
        /*
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.position += Vector3.left * 0.1f;
        if (Input.GetKey(KeyCode.RightArrow))
            transform.position += Vector3.right * 0.1f;
        if (Input.GetKey(KeyCode.UpArrow))
            transform.position += Vector3.forward * 0.1f;
        if (Input.GetKey(KeyCode.DownArrow))
            transform.position += Vector3.back * 0.1f;
        */

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up * 3);
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.Rotate(Vector3.down * 3);

        //if (Input.GetKey(KeyCode.R))
          //  transform.Rotate(Vector3.up * 3);

        if (Input.GetKey(KeyCode.A))
            transform.localScale += Vector3.one * (-0.1f);
        if (Input.GetKey(KeyCode.S))
            transform.localScale += Vector3.one * 0.1f;

        if (Input.GetKey(KeyCode.Space))
        {
            if (location_my.y <= 0)
                jump = true;
        }

        if (location_my.y <= 5 && jump)
            transform.position += Vector3.up * 0.5f;
        else jump = false;

        if (location_my.y > 0 && !jump)
            transform.position -= Vector3.up * 0.5f;
    }
}
