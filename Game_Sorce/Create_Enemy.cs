using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Create_Enemy : MonoBehaviour {
    //List<GameObject> List_Enemy = new List<GameObject>();
    List<GameObject> ListAstroid = new List<GameObject>();
	List<GameObject> ListItem = new List<GameObject>();

    public GameObject Astroid_1;
    public GameObject Astroid_2;
    public GameObject Astroid_3;
	public GameObject Item_Speed;
	public GameObject Item_Power;
	public GameObject Item_Boom;
    public GameObject Enemy;
    public GameObject Player;
    public float MinDelay_Astroid;
    public float MaxDelay_Astroid;
	public float MinDelay_Item;
	public float MaxDelay_Item;
    public Vector3 Player_Location;

    private float Cooltime_Ast;
	private float Delay_Astroid = 0.0f;
	private float Cooltime_Im;
	private float Delay_Item = 0.0f;

    private float PlayTime = 0.0f;

    private bool GameOver = false;
    
    // Use this for initialization
    void Start () {
        ListAstroid.Add(Astroid_1);
        ListAstroid.Add(Astroid_2);
        ListAstroid.Add(Astroid_3);

		ListItem.Add (Item_Speed);
		ListItem.Add (Item_Power);
		ListItem.Add (Item_Boom);

		Cooltime_Ast = Random.Range(MinDelay_Astroid, MaxDelay_Astroid);
		Cooltime_Im = Random.Range (MinDelay_Item, MaxDelay_Item);

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
			GameObject.Find("TimeText").transform.FindChild("Time").GetComponent<Text>().text = "0";

            GameOver = false;
			PlayTime = Delay_Astroid = Delay_Item = 0.0f;

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

	private void Create_Item()
	{
		int index = Random.Range(0, 100) % 3;
		float posX = Random.Range(-7.0f, 7.0f);

		GameObject obj = (GameObject)Instantiate(ListItem[index]);

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

    private void Cooltime_Astroid()
    {
		if (Delay_Astroid >= Cooltime_Ast)
        {
			Cooltime_Ast = Random.Range(MinDelay_Astroid, MaxDelay_Astroid);
			Delay_Astroid = 0.0f;

            Create_Astroid();

            return;
        }
        
		Delay_Astroid += Time.deltaTime;
    }

	private void Cooltime_Item()
	{
		if (Delay_Item >= Cooltime_Im)
		{
			Cooltime_Im = Random.Range(MinDelay_Item, MaxDelay_Item);
			Delay_Item = 0.0f;

			Create_Item();

			return;
		}

		Delay_Item += Time.deltaTime;
	}

    // Update is called once per frame
    void Update () {
        //Delete_Enemy();
        if (!GameOver)
        {
            Cooltime_Astroid();
			Cooltime_Item ();
            Alive_Player();

			PlayTime += Time.deltaTime;

			GameObject.Find("TimeText").transform.FindChild("Time").GetComponent<Text>().text = ((int)PlayTime).ToString();
        }
    }
}
