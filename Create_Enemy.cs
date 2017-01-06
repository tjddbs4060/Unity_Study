using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_Enemy : MonoBehaviour {
    //List<GameObject> List_Enemy = new List<GameObject>();
    List<GameObject> ListAstroid = new List<GameObject>();

    public GameObject Astroid_1;
    public GameObject Astroid_2;
    public GameObject Astroid_3;
    public GameObject Enemy;
    public GameObject Player;
    public float MinDelay;
    public float MaxDelay;
    public Vector3 Player_Location;

    private float Cooltime;
    private float Delay = 0.0f;

    private bool GameOver = false;
    
    // Use this for initialization
    void Start () {
        ListAstroid.Add(Astroid_1);
        ListAstroid.Add(Astroid_2);
        ListAstroid.Add(Astroid_3);

        Cooltime = Random.Range(MinDelay, MaxDelay);

        Create_Player();

        Invoke("Add_Enemy_Create", 1.0f);
    }
    /*
    private void Delete_Enemy()
    {
        List<GameObject> deleteList = new List<GameObject>();

        foreach (GameObject obj in List_Enemy)
        {
            if (obj.transform.position.z < -13)
                deleteList.Add(obj);
                //List_Missile_e_cooltime.RemoveAt(count);
        }

        foreach (GameObject obj in deleteList)
        {
            List_Enemy.Remove(obj);
            Destroy(obj);
        }

        deleteList.Clear();
    }
    */

    //Game Restart
    public void Restart()
    {
        if (GameOver)
        {
            GameObject.Find("ScoreText").transform.FindChild("Score").GetComponent<Text>().text = "0";

            GameOver = false;
            
            Create_Player();

            Invoke("Add_Enemy_Create", 1.0f);
        }
    }

    //Create Player
    private void Create_Player()
    {
        GameObject obj = (GameObject)Instantiate(Player);

        obj.transform.position = Player_Location;
        obj.name = "vehicle_playerShip";
        obj.SetActive(true);
    }

    //Player is Alive?
    private void Alive_Player()
    {
        if (!GameObject.Find("vehicle_playerShip"))
        {
            GameOver = true;
            CancelInvoke("Add_Enemy_Create");

            for (int i = 0; i < GameObject.Find("GameObject").transform.childCount; i++)
                Destroy(GameObject.Find("GameObject").transform.GetChild(i).gameObject);
        }
    }

    //Create Astroid
    private void Create_Astroid()
    {
        int index = Random.Range(0, 100) % 3;
        float posX = Random.Range(-7.0f, 7.0f);

        GameObject obj = (GameObject)Instantiate(ListAstroid[index]);
        
        obj.transform.SetParent(GameObject.Find("GameObject").transform);
        obj.transform.position = new Vector3(posX, 2, 12);
    }

    private void Add_Enemy_Create()
    {
        Invoke("Add_Enemy_Create", 1.0f);

        float posX = Random.Range(-7.0f, 7.0f);
        GameObject obj = (GameObject)Instantiate(Enemy);

        obj.transform.SetParent(GameObject.Find("GameObject").transform);
        obj.SetActive(true);
        obj.transform.position = new Vector3(posX, 2, 12);

        //List_Enemy.Add(obj);
        //List_Missile_e_cooltime.Add(Cooltime_e);
    }

    void Cooltime_Astroid()
    {
        if (Delay >= Cooltime)
        {
            Cooltime = Random.Range(MinDelay, MaxDelay);
            Delay = 0.0f;

            Create_Astroid();

            return;
        }

        Delay += Time.deltaTime;
    }


    // Update is called once per frame
    void Update () {
        //Delete_Enemy();
        if (!GameOver)
        {
            Cooltime_Astroid();
            Alive_Player();
        }
    }
}
