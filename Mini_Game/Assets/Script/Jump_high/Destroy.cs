using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{

    public GameObject Player;
    public GameObject platform;
    private GameObject myplat;
    public GameObject spring;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //유지 보수
        if (collision.gameObject.name.StartsWith("Platform"))
        {
            if(Random.Range(1,7) == 1)
            {
                //삭제하고 스프링생성
                Destroy(collision.gameObject);
                Instantiate(spring, new Vector2(Random.Range(-4.5f, 4.5f), Player.transform.position.y + (14 + Random.Range(0.2f, 1.0f))), Quaternion.identity);
            }

            else
            {
                //위치변경
                collision.gameObject.transform.position = new Vector2(Random.Range(-4.5f, 4.5f), Player.transform.position.y + (14 + Random.Range(0.2f, 1.0f)));
            }
        }

        else if(collision.gameObject.name.StartsWith("Spring"))
        {
            if (Random.Range(1, 7) == 1)
            {
                //위치변경
                collision.gameObject.transform.position = new Vector2(Random.Range(-4.5f, 4.5f), Player.transform.position.y + (14 + Random.Range(0.2f, 1.0f)));
            }

            else
            {
                //스프링 지우고 일반 발판
                Destroy(collision.gameObject);
                Instantiate(platform, new Vector2(Random.Range(-4.5f, 4.5f), Player.transform.position.y + (14 + Random.Range(0.2f, 1.0f))), Quaternion.identity);
            }
        }
        
        //생성하고 지우고 반복
        /*//Destory 내장 함수
        Destroy(collision.gameObject);

        if (Random.Range(1, 6) > 1)
        {
            myplat = (GameObject)Instantiate(platform, new Vector2(Random.Range(-5.5f, 5.5f), Player.transform.position.y + (14 + Random.Range(0.5f, 1f))), Quaternion.identity);
        }
        else
        {
            myplat = (GameObject)Instantiate(spring, new Vector2(Random.Range(-5.5f, 5.5f), Player.transform.position.y + (14 + Random.Range(0.5f, 1f))), Quaternion.identity);
        }
        */

        
    }
}
