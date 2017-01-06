using UnityEngine;
using System.Collections;

public class Done_Mover : MonoBehaviour
{
    public GameObject Player;
	public float speed;
    
	void Start ()
	{
		GetComponent<Rigidbody>().velocity = transform.forward * speed;
	}

    void Update()
    {
        if (transform.position.z < -20 || transform.position.z > 20)
            Destroy(gameObject);
    }
}
