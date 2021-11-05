using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onclick_menu : MonoBehaviour
{
    public GameObject Selectmenu;
    public GameObject Optionmenu;
    public GameObject Exitmenu;


    // Start is called before the first frame update
    public void option_btn_clicked()
    {
        Selectmenu.SetActive(false);
        Optionmenu.SetActive(true);
    }

    public void apply_btn_clicked()
    {
        Optionmenu.SetActive(false);
        Selectmenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {

            }

            else if (Input.GetKey(KeyCode.Escape))
            {
                Exitmenu.SetActive(true);
            }

            else if (Input.GetKey(KeyCode.Menu))
            {

            }
        }
    }
}
