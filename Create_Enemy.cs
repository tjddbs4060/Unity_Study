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
    public float MinDelay_Ast;
    public float MaxDelay_Ast;
    public float MinDelay_Item;
    public float MaxDelay_Item;
    public Vector3 Player_Location;
    public Text ID;

    private float Cooltime_Ast;
    private float Delay_Ast = 0.0f;
    private float Cooltime_Item;
    private float Delay_Item = 0.0f;
    private float PlayTime = 0.0f;

    private string Save_File_Path = @"C:\Users\Administrator\Documents\New Unity Project\Assets\Shooting Game Sorce\Save\Score.txt";

    private bool GameOver = true;
    
    // Use this for initialization
    void Start () {
        ListAstroid.Add(Astroid_1);
        ListAstroid.Add(Astroid_2);
        ListAstroid.Add(Astroid_3);

        ListItem.Add(Item_Speed);
        ListItem.Add(Item_Power);
        ListItem.Add(Item_Boom);

        Cooltime_Ast = Random.Range(MinDelay_Ast, MaxDelay_Ast);
        Cooltime_Item = Random.Range(MinDelay_Item, MaxDelay_Item);

        //Create_Player();

        //Invoke("Add_Enemy_Create", 1.0f);
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
    
    class Name_Score
    {
        public string name;
        public int score;
    }

    private void Score_Sort()
    {
        List<Name_Score> List_name_score = new List<Name_Score>();
        
        string[] score_text = System.IO.File.ReadAllLines(Save_File_Path);

        for (int i = 0; i < score_text.Length; i++)
        {
            Name_Score name_score = new Name_Score();

            name_score.name = score_text[i].Split(' ')[0];
            name_score.score = int.Parse(score_text[i].Split(' ')[2]);

            List_name_score.Add(name_score);
        }

        Name_Score temp;

        for (int i = 0; i < List_name_score.Count; i++)
        {
            for (int j = i; j < List_name_score.Count; j++)
            {
                if (List_name_score[i].score < List_name_score[j].score)
                {
                    temp = List_name_score[i];
                    List_name_score[i] = List_name_score[j];
                    List_name_score[j] = temp;
                }
            }
        }
        
        for (int i = 0; i < GameObject.Find("Canvas").transform.FindChild("ScoreView").transform.FindChild("Viewport").transform.FindChild("Content").transform.childCount; i++)
            Destroy(GameObject.Find("Canvas").transform.FindChild("ScoreView").transform.FindChild("Viewport").transform.FindChild("Content").transform.GetChild(i).gameObject);

        System.IO.File.WriteAllText(Save_File_Path, "");
        
        foreach (Name_Score nm in List_name_score)
        {
            System.IO.File.AppendAllText(Save_File_Path, nm.name + " : " + nm.score + "\r\n", System.Text.Encoding.Default);

            Text text = (Text)Instantiate(ID);
            
            text.transform.SetParent(GameObject.Find("Canvas").transform.FindChild("ScoreView").transform.FindChild("Viewport").transform.FindChild("Content").transform);
            text.text = nm.name + " : " + nm.score;
        }
    }

    public void Save()
    {
        //string Id = GameObject.Find("Canvas").transform.FindChild("ScoreView").transform.FindChild("Input_ID").transform.FindChild("Text").GetComponent<Text>().text;
        string Score = GameObject.Find("Canvas").transform.FindChild("ScoreText").transform.FindChild("Score").GetComponent<Text>().text;

        System.IO.File.AppendAllText(Save_File_Path, ID.text + " : " + Score + "\r\n", System.Text.Encoding.Default);

        Score_Sort();

        GameObject.Find("ScoreView").SetActive(false);
    }
    
    public void Game_Start()
    {
        GameObject.Find("Canvas").transform.FindChild("Title").gameObject.SetActive(false);
        
        Restart();
    }

    //Game Restart
    public void Restart()
    {
        if (GameOver)
        {
            GameObject.Find("ScoreText").transform.FindChild("Score").GetComponent<Text>().text = "0";
            GameObject.Find("TimeText").transform.FindChild("Time").GetComponent<Text>().text = "0";

            PlayTime = Delay_Ast = Delay_Item = 0.0f;

            GameOver = false;

            Score_Sort();

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
            GameObject.Find("Canvas").transform.FindChild("ScoreView").gameObject.SetActive(true);

            for (int i = 0; i < GameObject.Find("GameObject").transform.childCount; i++)
                Destroy(GameObject.Find("GameObject").transform.GetChild(i).gameObject);

            for (int i = 0; i < GameObject.Find("Canvas").transform.FindChild("Top_Background").transform.childCount; i++)
                Destroy(GameObject.Find("Canvas").transform.FindChild("Top_Background").transform.GetChild(i).gameObject);
        }
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

    //Create Item
    private void Create_Item()
    {
        int index = Random.Range(0, 100) % 3;
        float posX = Random.Range(-7.0f, 7.0f);

        GameObject obj = (GameObject)Instantiate(ListItem[index]);

        obj.transform.SetParent(GameObject.Find("GameObject").transform);
        obj.transform.position = new Vector3(posX, 2, 12);
    }

    void Cooltime_Im()
    {
        if (Delay_Item >= Cooltime_Item)
        {
            Cooltime_Item = Random.Range(MinDelay_Item, MaxDelay_Item);
            Delay_Item = 0.0f;

            Create_Item();

            return;
        }

        Delay_Item += Time.deltaTime;
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
    
    void Cooltime_Astroid()
    {
        if (Delay_Ast >= Cooltime_Ast)
        {
            Cooltime_Ast = Random.Range(MinDelay_Ast, MaxDelay_Ast);
            Delay_Ast = 0.0f;

            Create_Astroid();

            return;
        }

        Delay_Ast += Time.deltaTime;
    }

    private void Time_Flow()
    {
        PlayTime += Time.deltaTime;

        GameObject.Find("TimeText").transform.FindChild("Time").GetComponent<Text>().text = ((int)PlayTime).ToString();
    }

    // Update is called once per frame
    void Update () {
        //Delete_Enemy();
        if (!GameOver)
        {
            Cooltime_Astroid();
            Cooltime_Im();
            Alive_Player();
            Time_Flow();
        }
    }
}
