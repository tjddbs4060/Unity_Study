using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {
    //List<GameObject> List_Missile_e = new List<GameObject>();

    public GameObject Missile_e;
    public GameObject Explosion;

    private float Cooltime_e;
    private float Move_Speed;
    private float Missile_Speed;
    private float Delay_e;

    // Use this for initialization
    void Start () {
        Move_Speed = Random.Range(3.0f, 7.0f);
        Delay_e = Random.Range(0.5f, 3.0f);
        Missile_Speed = Random.Range(-30.0f, -10.0f);

        Invoke("Add_Enemy_Missile", Delay_e);
    }

    private void OnDestroy()
    {
        GameObject obj = (GameObject)Instantiate(Explosion);
        obj.transform.position = transform.position;
    }

    //Delete Self
    private void Delete_Self()
    {
        if (transform.position.z < -20)
            Destroy(gameObject);
    }

    //Collision
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Player_Bullet":
                /*
                List<GameObject> deleteList = new List<GameObject>();

                foreach (GameObject obj in List_Missile_e)
                {
                    deleteList.Add(obj);
                }

                foreach (GameObject obj in deleteList)
                {
                    List_Missile_e.Remove(obj);
                    Destroy(obj);
                }

                deleteList.Clear();
                */
                collision.gameObject.SetActive(false);
                Destroy(gameObject);

                break;
            case "Player":
                Destroy(gameObject);
                break;
        }
    }

    //Delete Enemy's Missile
    /*
    private void Delete_Missile()
    {
        List<GameObject> deleteList = new List<GameObject>();
        
        foreach (GameObject obj in List_Missile_e)
        {
            if (obj.transform.position.z < -12)
                deleteList.Add(obj);
        }

        foreach(GameObject obj in deleteList)
        {
            List_Missile_e.Remove(obj);
            Destroy(obj);
        }

        deleteList.Clear();
    }
    */
    //Add Enemy's Missile
    private void Add_Enemy_Missile()
    {
        Invoke("Add_Enemy_Missile", Delay_e);

        GameObject obj = (GameObject)Instantiate(Missile_e);

        Done_Mover mover = obj.GetComponent<Done_Mover>();
        mover.speed = Missile_Speed;

        obj.SetActive(true);
        obj.transform.position = transform.position;

        //List_Missile_e.Add(obj);
    }

    //Enemy Move
    private void Move_Enemy()
    {
        transform.position += Vector3.back * Time.deltaTime * Move_Speed;
    }

    // Update is called once per frame
    void Update () {
        //Delete_Missile();
        Move_Enemy();
        Delete_Self();
    }
}
