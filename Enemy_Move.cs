using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Move : MonoBehaviour {
    //List<GameObject> List_Missile_e = new List<GameObject>();
    public GameObject Missile_e;
    public GameObject Explosion;

    private float Cooltime_e;
    private float Move_Speed;
    private float Missile_Speed;
    private float Delay_e;

    private int Move_pattern;
    private float range;

    // Use this for initialization
    void Start () {
        Move_Speed = Random.Range(3.0f, 7.0f);
        Delay_e = Random.Range(0.5f, 3.0f);
        Missile_Speed = Random.Range(-30.0f, -10.0f);
        Move_pattern = (int)(Random.Range(0.0f, 3.0f));
        range = Random.Range(2.0f, 10.0f);

        Invoke("Add_Enemy_Missile", Delay_e);
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
        GameObject obj = (GameObject)Instantiate(Explosion);
        
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
                string score = GameObject.Find("ScoreText").transform.FindChild("Score").GetComponent<Text>().text;
                int score_result = int.Parse(score) + 1;

                score = score_result.ToString();

                GameObject.Find("ScoreText").transform.FindChild("Score").GetComponent<Text>().text = score;

                obj.transform.position = transform.position;

                Destroy(collision.gameObject);
                Destroy(gameObject);

                break;
            case "Player":
                obj.transform.position = transform.position;

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

        obj.transform.SetParent(GameObject.Find("GameObject").transform);
        obj.SetActive(true);
        obj.transform.position = transform.position;

        //List_Missile_e.Add(obj);
    }

    //Enemy Move
    private float move = 0.0f;

    private void Move_Enemy()
    {
        move += Time.deltaTime;

        transform.position += Vector3.back * Time.deltaTime * Move_Speed;

        Pattern();
    }

    private void Pattern()
    {
        float xpos = Mathf.Sin(move * Mathf.Deg2Rad * 50.0f) * range;
        float zpos = Mathf.Cos(move * Mathf.Deg2Rad * 50.0f) * range;
        switch (Move_pattern)
        {
            case 0:
                transform.position = new Vector3(xpos, transform.position.y, transform.position.z);

                break;
            case 1:
                transform.position = new Vector3(xpos, transform.position.y, zpos);

                break;
            case 2:
                transform.position = new Vector3(transform.position.x, transform.position.y, zpos);

                break;
        }
    }

    // Update is called once per frame
    void Update () {
        //Delete_Missile();
        Move_Enemy();
        Delete_Self();
    }
}
