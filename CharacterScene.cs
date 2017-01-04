using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterScene : MonoBehaviour {
    List<GameObject> List_Character = new List<GameObject>();
    List<Toggle> List_Toggle = new List<Toggle>();

    public Toggle tog_button;
    public GameObject select_box;
    public GameObject dummy;
    private GameObject select_cur;
	// Use this for initialization
	void Start () {
		
	}
    
    private void Reset_Character()
    {
        if (select_cur == null)
            return;
        
        select_cur.transform.eulerAngles = new Vector3(0, 180, 0);
    }

    public void SetToggle(bool value)
    {
        if (!value)
            return;

        for (int i = 0; i < List_Toggle.Count; i++)
        {
            if (List_Toggle[i].isOn)
            {
                Reset_Character();

                select_cur = List_Character[i];

                select_box.SetActive(true);
                select_box.transform.position = new Vector3(i - 2, 0, 0);

                break;
            }
        }
    }

    public void Add_Character()
    {
        if (List_Character.Count >= 5)
            return;

        GameObject obj = (GameObject)Instantiate(dummy);

        obj.transform.position = new Vector3(List_Character.Count - 2, 0, 0);
        obj.SetActive(true);

        List_Character.Add(obj);

        Toggle tog = (Toggle)Instantiate(tog_button);

        tog.transform.SetParent(tog_button.transform.parent);
        //tog.transform.parent = tog_button.transform.parent;
        tog.transform.localPosition = new Vector3(List_Toggle.Count * 80 - 150, 70, 0);
        tog.gameObject.SetActive(true);

        tog.transform.GetComponentInChildren<Text>().text = (List_Toggle.Count+1).ToString();

        List_Toggle.Add(tog.GetComponent<Toggle>());
        /*
        GameObject tog = (GameObject)Instantiate(tog_button.gameObject);

        tog.transform.parent = tog_button.transform.parent;
        tog.transform.localPosition = new Vector3(List_Toggle.Count * 70 - 150, 70, 0);

        List_Toggle.Add(tog.GetComponent<Toggle>());
        */
    }

    public void Rotate(int value)
    {
        if (select_cur == null)
            return;

        select_cur.transform.Rotate(Vector3.up * value);
    }
    /*
    public void Rotate_Right()
    {
        if (select_cur == null)
            return;

        select_cur.transform.Rotate(Vector3.down * 10);
    }
    */
	// Update is called once per frame
	void Update () {
        /*
		if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Reset_Character();

            if (List_Character.Count > 0)
                select_cur = List_Character[0];
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Reset_Character();

            if (List_Character.Count > 1)
                select_cur = List_Character[1];
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Reset_Character();

            if (List_Character.Count > 2)
                select_cur = List_Character[2];
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Reset_Character();

            if (List_Character.Count > 3)
                select_cur = List_Character[3];
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Reset_Character();

            if (List_Character.Count > 4)
                select_cur = List_Character[4];
        }
        */  
    }
}
