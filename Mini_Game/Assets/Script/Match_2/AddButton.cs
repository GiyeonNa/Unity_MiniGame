using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButton : MonoBehaviour
{
    [SerializeField]
    private Transform puzzlefield;

    [SerializeField]
    private GameObject btn;

    private void Awake()
    {
        for(int i=0; i<9; i++)
        {
            //add Button
            //copy 
            GameObject button = Instantiate(btn);

            button.name = "" + (i);

            button.transform.SetParent(puzzlefield, false);
        }
    }
    
}
