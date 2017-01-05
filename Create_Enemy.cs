using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_Enemy : MonoBehaviour {
    //List<GameObject> List_Enemy = new List<GameObject>();

    public GameObject Enemy;
    // Use this for initialization
    void Start () {
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
    private void Add_Enemy_Create()
    {
        Invoke("Add_Enemy_Create", 1.0f);

        float posX = Random.Range((float)-7.0, (float)7.0);
        GameObject obj = (GameObject)Instantiate(Enemy);

        obj.SetActive(true);
        obj.transform.position = new Vector3(posX, 2, 12);

        //List_Enemy.Add(obj);
        //List_Missile_e_cooltime.Add(Cooltime_e);
    }

    // Update is called once per frame
    void Update () {
        //Delete_Enemy();
    }
}
