using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {
    List<GameObject> List_Missile_p = new List<GameObject>();
    List<GameObject> List_Enemy = new List<GameObject>();
    List<GameObject> List_Missile_e = new List<GameObject>();
    List<float> List_Missile_e_cooltime = new List<float>();

    public GameObject Player;
    public GameObject Missile_p;
    public GameObject Enemy;
    public GameObject Missile_e;

    private const float Cooltime_p = 1.0f;
    private const float Cooltime_e = 2.0f;
    private const float Player_Speed = 5.0f;
    private float Delay_p = 1.0f;
	// Use this for initialization
	void Start () {
        Invoke("Add_Enemy_Create", 1.0f);
        //Invoke("CoolTime", 0.01f);
        //StartCoroutine("CreateEnemy");
        //StartCoroutine("CreateEnemy", 1.0f);//변수
    }
    /*
    public IEnumerator CreateEnemy()    //코르틴
    {
        while(true)
        {
            GameObject obj = GameObject.Instantiate(Enemy);
            List_Enemy.Add(obj);
            yield return new WaitForSeconds(1.0f);
        }
    }
    */
    
    private void Enemy_Move()
    {
        for (int i = 0; i < List_Enemy.Count; i++)
            List_Enemy[i].transform.position += Vector3.back * 0.1f;
    }

    private void CoolTime()
    {
        //Invoke("CoolTime", 0.01f);
        //Player CoolTime
        if (Delay_p >= Cooltime_p)
            Delay_p = Cooltime_p;

        //Enemy CoolTime
        for (int i = 0; i < List_Missile_e_cooltime.Count; i++)
        {
            if (List_Missile_e_cooltime[i] >= Cooltime_e)
            {
                Add_Enemy_Missile(List_Enemy[i].transform.position);
                List_Missile_e_cooltime[i] = 0;
            }

            List_Missile_e_cooltime[i] += Time.deltaTime;
        }

        Delay_p += Time.deltaTime;
    }

    private void Add_Enemy_Missile(Vector3 pos_Enemy)
    {
        GameObject obj = (GameObject)Instantiate(Missile_e);

        obj.SetActive(true);
        obj.transform.position = pos_Enemy;

        List_Missile_e.Add(obj);
    }

    private void Add_Enemy_Create()
    {
        Invoke("Add_Enemy_Create", 1.0f);

        float posX = Random.Range((float)-7.0, (float)7.0);
        GameObject obj = (GameObject)Instantiate(Enemy);
        
        obj.SetActive(true);
        obj.transform.position = new Vector3(posX, 0, 10);

        List_Enemy.Add(obj);
        List_Missile_e_cooltime.Add(Cooltime_e);
    }

    private void Delete_Obj(List<GameObject> obj, List<GameObject> del)
    {
        foreach (GameObject i in del)
        {
            obj.Remove(i);
            Destroy(i);
        }

        del.Clear();
    }

    private void Delete_Missile()
    {
        List<GameObject> deleteList = new List<GameObject>();
        int count = 0;

        foreach (GameObject obj in List_Missile_p)
        {
            if (obj.transform.position.z > 12)
                deleteList.Add(obj);
        }

        Delete_Obj(List_Missile_p, deleteList);

        foreach (GameObject obj in List_Missile_e)
        {
            if (obj.transform.position.z < -12)
                deleteList.Add(obj);
        }

        Delete_Obj(List_Missile_e, deleteList);

        foreach (GameObject obj in List_Enemy)
        {
            if (obj.transform.position.z < -13)
            {
                deleteList.Add(obj);
                List_Missile_e_cooltime.RemoveAt(count);
            }
            count++;
        }

        Delete_Obj(List_Enemy, deleteList);

        /*
        for (int i = 0; i < List_Missile_p.Count; i++)
        {
            if (List_Missile_p[i].transform.position.z > 12)
            {
                Destroy(List_Missile_p[i]);
                List_Missile_p.RemoveAt(i);
            }
        }

        for (int i = 0; i < List_Missile_e.Count; i++)
        {
            if (List_Missile_e[i].transform.position.z < -12)
            {
                Destroy(List_Missile_e[i]);
                List_Missile_e.RemoveAt(i);
            }
        }
        
        for (int i = 0; i < List_Enemy.Count; i++)
        {
            if (List_Enemy[i].transform.position.z < -13)
            {
                Destroy(List_Enemy[i]);
                List_Enemy.RemoveAt(i);
                
                List_Missile_e_cooltime.RemoveAt(i);
            }
        }
        */

    }

    private void Add_Missile()
    {
        if (Delay_p < 1.0f)
            return;

        GameObject obj = (GameObject)Instantiate(Missile_p);

        obj.SetActive(true);
        obj.transform.position = Player.transform.position;

        List_Missile_p.Add(obj);

        Delay_p = 0;
    }

    private void Air_Move(float value)
    {
        Player.transform.position += Vector3.right * Time.deltaTime * value;

        if (Player.transform.position.x < -7)
            Player.transform.position -= Vector3.right * Time.deltaTime * value;
        else if (Player.transform.position.x > 7)
            Player.transform.position -= Vector3.right * Time.deltaTime * value;
    }

	// Update is called once per frame
	void Update () {
        Delete_Missile();
        Enemy_Move();
        CoolTime();

        if (Input.GetKey(KeyCode.LeftArrow))
            Air_Move(-Player_Speed);

        if (Input.GetKey(KeyCode.RightArrow))
            Air_Move(Player_Speed);

        if (Input.GetKey(KeyCode.Space))
            Add_Missile();

    }
}
