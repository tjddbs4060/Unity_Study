using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : MonoBehaviour {
    //List<GameObject> List_Missile_p = new List<GameObject>();
    //List<GameObject> List_Enemy = new List<GameObject>();
    //List<GameObject> List_Missile_e = new List<GameObject>();
    //List<float> List_Missile_e_cooltime = new List<float>();

    //public GameObject Player;
    public GameObject Missile_p;
    public GameObject Explosion;
    //public GameObject Enemy;
    //public GameObject Missile_e;

    private float Cooltime_p = 1.0f;
	private int Bullet = 1;
	private int Boom = 1;
    //private const float Cooltime_e = 2.0f;
    private const float Player_Speed = 5.0f;
    private Vector3 First_location;
    private float Delay_p = 1.0f;

	// Use this for initialization
	void Start () {
        //Invoke("Add_Enemy_Create", 1.0f);
        //Invoke("CoolTime", 0.01f);
        //StartCoroutine("CreateEnemy");
        //StartCoroutine("CreateEnemy", 1.0f);//변수
        First_location = gameObject.transform.position;
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

    /*
    //Enemy Move
    private void Enemy_Move()
    {
        for (int i = 0; i < List_Enemy.Count; i++)
            List_Enemy[i].transform.position += Vector3.back * 0.1f;
    }
    */

    private void Destroy_Player()
    {
        GameObject obj = (GameObject)Instantiate(Explosion);
        obj.transform.position = transform.position;
    }

    //Collision
    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Enemy_Bullet" :
                Destroy(collision.gameObject);
                Destroy(gameObject);

                Destroy_Player();
                break;
            case "Enemy":
                Destroy(gameObject);

                Destroy_Player();
                break;
        }
    }

    //Player & Enumy Attack CoolTime
    private void CoolTime()
    {
        //Invoke("CoolTime", 0.01f);
        //Player CoolTime
        if (Delay_p >= Cooltime_p)
            Delay_p = Cooltime_p;

        //Enemy CoolTime
        /*
        for (int i = 0; i < List_Missile_e_cooltime.Count; i++)
        {
            if (List_Missile_e_cooltime[i] >= Cooltime_e)
            {
                Add_Enemy_Missile(List_Enemy[i].transform.position);
                List_Missile_e_cooltime[i] = 0;
            }

            List_Missile_e_cooltime[i] += Time.deltaTime;
        }
        */
        Delay_p += Time.deltaTime;
    }

    //Add Enemy's Missile Create
    /*
    private void Add_Enemy_Missile(Vector3 pos_Enemy)
    {
        GameObject obj = (GameObject)Instantiate(Missile_e);

        obj.SetActive(true);
        obj.transform.position = pos_Enemy;

        List_Missile_e.Add(obj);
    }
    */
    //Add Enemy Create
    /*
    private void Add_Enemy_Create()
    {
        Invoke("Add_Enemy_Create", 1.0f);

        float posX = Random.Range((float)-7.0, (float)7.0);
        GameObject obj = (GameObject)Instantiate(Enemy);
        
        obj.SetActive(true);
        obj.transform.position = new Vector3(posX, 0, 10);

        List_Enemy.Add(obj);
        //List_Missile_e_cooltime.Add(Cooltime_e);
    }
    */
    private void Delete_Obj(List<GameObject> obj, List<GameObject> del)
    {
        foreach (GameObject i in del)
        {
            obj.Remove(i);
            Destroy(i);
        }

        del.Clear();
    }

    //Delete Player & Enemy's Missile & Enemy
    /*
    private void Delete_Missile()
    {
        List<GameObject> deleteList = new List<GameObject>();
        //int count = 0;

        foreach (GameObject obj in List_Missile_p)
        {
            if (obj.transform.position.z > 13)
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
                //List_Missile_e_cooltime.RemoveAt(count);
            }
            count++;
        }

        Delete_Obj(List_Enemy, deleteList);
        
        
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
    }
    */

	private void Take_Speed()
	{
		if (Cooltime_p <= 2.0f) {
			Cooltime_p = 2.0f;

			string score = GameObject.Find("ScoreText").transform.FindChild("Score").GetComponent<Text>().text;
			int score_result = int.Parse(score) + 5;

			score = score_result.ToString();

			GameObject.Find("ScoreText").transform.FindChild("Score").GetComponent<Text>().text = score;
		}
		Cooltime_p -= 0.5f;
	}

    //Add Player Attack
    private void Add_Missile()
    {
        if (Delay_p < 1.0f)
            return;

		for (int i = 0; i < Bullet; i++) {	//총알 개수에 따른 위치 지정하려다가 말음
			GameObject obj = (GameObject)Instantiate (Missile_p);
        
			obj.transform.SetParent (GameObject.Find ("GameObject").transform);
			obj.SetActive (true);
			obj.transform.position = transform.position;
		}
        //List_Missile_p.Add(obj);

        Delay_p = 0;
    }

    //Player Move
    private void Air_Move_LR(float value)
    {
        transform.position += Vector3.right * Time.deltaTime * value;

        if (transform.position.x < -7)
            transform.position -= Vector3.right * Time.deltaTime * value;
        else if (transform.position.x > 7)
            transform.position -= Vector3.right * Time.deltaTime * value;
    }

    //Player Move
    private void Air_Move_UD(float value)
    {
        transform.position += Vector3.forward * Time.deltaTime * value;

        if (transform.position.z <= -10)
            transform.position -= Vector3.forward * Time.deltaTime * value;
        else if (transform.position.z >= 10)
            transform.position -= Vector3.forward * Time.deltaTime * value;
    }

    // Update is called once per frame
    void Update () {
        //Delete_Missile();
        //Enemy_Move();
        CoolTime();

        if (Input.GetKey(KeyCode.LeftArrow))
            Air_Move_LR(-Player_Speed);

        if (Input.GetKey(KeyCode.RightArrow))
            Air_Move_LR(Player_Speed);

        if (Input.GetKey(KeyCode.UpArrow))
            Air_Move_UD(Player_Speed);

        if (Input.GetKey(KeyCode.DownArrow))
            Air_Move_UD(-Player_Speed);

        if (Input.GetKey(KeyCode.Space))
            Add_Missile();
    }
}


/*
 * 스크립트 컴포넌트 가져오는 방법.
    docs.unity3d.com/kr/current/ScriptReference/Component.GetComponent.html
GameObject obj = GameObject.Instantiate(missile);
Done_Mover mover = obj.GetComponent<Done_Mover>();
mover.speed = -10;

*/
