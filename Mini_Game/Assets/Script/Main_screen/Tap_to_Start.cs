using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tap_to_Start : MonoBehaviour
{
    public GameObject readText;
    public static Tap_to_Start instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if(Tap_to_Start.instance == null)
        {
            Tap_to_Start.instance = this;
        }
    }

    private void Start()
    {
        readText.SetActive(false);
        StartCoroutine(ShowReady());
    }

    IEnumerator ShowReady()
    {
        while (true)
        {
            readText.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            readText.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tap_start()
    {
        SceneManager.LoadScene("Select");
    }
}
