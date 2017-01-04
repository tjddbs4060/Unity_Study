using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Duplicate : MonoBehaviour {
    List<GameObject> list = new List<GameObject>();

	public GameObject dummy;
    public GameObject select_cur;
    private Vector3 prePos;

    //public Toggle t;
    bool isOn = true;
    // Use this for initialization
    void Start () {
		
	}

    public bool SetToggle
    {
        set
        {
            isOn = value;
            print(value);
            dummy.SetActive(isOn);
            if (isOn)
                dummy.name = "On Dummy";
            else dummy.name = "Off Dummy";
        }
    }

    private void Character_Reset()
    {
        /*
        select_cur.transform.rotation = Quaternion.identity;
        select_cur.transform.localScale = Vector3.one;
        select_cur.transform.position = prePos;
        select_cur.SetActive(true);
        */
        for (int i = 0; i < list.Count; i++)
        {
            //list[i].transform.eulerAngles = new Vector3(0, 0, 0);
            list[i].transform.rotation = Quaternion.identity;
            list[i].transform.localScale = new Vector3(2, 2, 2);
            list[i].transform.position = new Vector3(i, 0, 0);
            list[i].SetActive(true);
        }
    }

    public void Add_Scale()
    {
        select_cur.transform.localScale += Vector3.one * 0.1f;
    }
    /*
    public void Toggle()
    {
        select_cur.SetActive(t.isOn);
    }
    */

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
            if (list.Count < 10)
            {
                Character_Reset();

                GameObject obj = (GameObject)Instantiate(dummy);
                obj.transform.position = new Vector3(list.Count, 0, 0);

                list.Add(obj);
                select_cur = list[list.Count - 1];
                prePos = select_cur.transform.position;
                //GameObject listObj = list[list.Count - 1];
                //Vector3 v = obj.transform.position;
            }
		}
        
        if (Input.GetKey(KeyCode.LeftArrow))
            select_cur.transform.Rotate(Vector3.up * 3);
        if (Input.GetKey(KeyCode.RightArrow))
            select_cur.transform.Rotate(Vector3.down * 3);

        if (Input.GetKey(KeyCode.UpArrow))
            select_cur.transform.position += Vector3.up * 0.5f;
        if (Input.GetKey(KeyCode.DownArrow))
            select_cur.transform.position += Vector3.down * 0.5f;

        if (Input.GetKey(KeyCode.S))
            select_cur.transform.localScale += Vector3.one * 0.1f;
        if (Input.GetKey(KeyCode.A))
            select_cur.transform.localScale -= Vector3.one * 0.1f;

        if (Input.GetKeyDown(KeyCode.Keypad0))
        {
            Character_Reset();

            if (list.Count < 1)
                select_cur = list[list.Count - 1];
            else select_cur = list[0];

            prePos = select_cur.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Character_Reset();

            if (list.Count < 2)
                select_cur = list[list.Count - 1];
            else select_cur = list[1];

            prePos = select_cur.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Character_Reset();

            if (list.Count < 3)
                select_cur = list[list.Count - 1];
            else select_cur = list[2];

            prePos = select_cur.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Character_Reset();

            if (list.Count < 4)
                select_cur = list[list.Count - 1];
            else select_cur = list[3];

            prePos = select_cur.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Character_Reset();

            if (list.Count < 5)
                select_cur = list[list.Count - 1];
            else select_cur = list[4];

            prePos = select_cur.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Character_Reset();

            if (list.Count < 6)
                select_cur = list[list.Count - 1];
            else select_cur = list[5];

            prePos = select_cur.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Character_Reset();

            if (list.Count < 7)
                select_cur = list[list.Count - 1];
            else select_cur = list[6];

            prePos = select_cur.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            Character_Reset();

            if (list.Count < 8)
                select_cur = list[list.Count - 1];
            else select_cur = list[7];

            prePos = select_cur.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            Character_Reset();

            if (list.Count < 9)
                select_cur = list[list.Count - 1];
            else select_cur = list[8];

            prePos = select_cur.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            Character_Reset();

            if (list.Count < 10)
                select_cur = list[list.Count - 1];
            else select_cur = list[9];

            prePos = select_cur.transform.position;
        }

        //Toggle();
    }
}
