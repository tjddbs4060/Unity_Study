using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {
    List<GameObject> List_Missile_e = new List<GameObject>();

    public GameObject Missile_e;

    private float Cooltime_e;
    private float Move_Speed;
    private float Delay_e;

    // Use this for initialization
    void Start () {
        Set_Init();
    }

    private void Set_Init()
    {
        Move_Speed = Random.Range(3.0f, 7.0f);
        Delay_e = Cooltime_e = Random.Range(0.5f, 3.0f);
    }

    //Enemy CoolTime
    private void CoolTime()
    {
        Delay_e += Time.deltaTime;
        
        if (Delay_e >= Cooltime_e)
        {
            Delay_e = 0;
            Add_Enemy_Missile();
        }
    }

    //Delete Enemy's Missile
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

    //Add Enemy's Missile
    private void Add_Enemy_Missile()
    {
        GameObject obj = (GameObject)Instantiate(Missile_e);

        obj.SetActive(true);
        obj.transform.position = transform.position;

        List_Missile_e.Add(obj);
    }

    //Enemy Move
    private void Move_Enemy()
    {
        transform.position += Vector3.back * Time.deltaTime * Move_Speed;
    }

    // Update is called once per frame
    void Update () {
        CoolTime();
        Delete_Missile();
        Move_Enemy();
    }
}
