using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move : MonoBehaviour {
    List<GameObject> List_Missile_e = new List<GameObject>();

    public GameObject Missile_e;

    private const float Move_Speed = 3.0f;
    private const float Cooltime_e = 2.0f;
    private float Delay_e = 2.0f;

    // Use this for initialization
    void Start () {
		
	}

    private void CoolTime()
    {
        Delay_e += Time.deltaTime;

        if (Delay_e >= Cooltime_e)
        {
            Delay_e = 0;
            Add_Enemy_Missile();
        }
    }

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

    private void Add_Enemy_Missile()
    {
        GameObject obj = (GameObject)Instantiate(Missile_e);

        obj.SetActive(true);
        obj.transform.position = transform.position;

        List_Missile_e.Add(obj);
    }

    // Update is called once per frame
    void Update () {
        CoolTime();
        Delete_Missile();

        transform.position += Vector3.back * Time.deltaTime * Move_Speed;
    }
}
