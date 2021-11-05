using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //Sprite form puzzles

    int ClickCount = 0;
    public Sprite[] puzzles;
    
    public List<Sprite> gamePuzzles = new List<Sprite>();
    
    //list형식Button
    public List<Button> btns = new List<Button>();

    //check
    private bool firstGuess;
    private bool secondGuess;

    private int countGuesses;
    private int countCorrectGuesses;
    private int gameGuesses;

    private int firstGuessIndex, secondGuessIndex;

    private string firstGuessPuzzle, secondGuessPuzzle;

    public int totalPoint;
    public Text UIPoint;

    public System.Random rand = new System.Random();

    //타이머
    public float LimitTime;
    public Text text_Timer;

    //sound
    AudioSource AudioSource;
    public AudioClip chosebtns;
    public AudioClip correctbtns;
    public AudioClip incorrectbtns;
    public AudioClip cancelbtns;

    //gameover
    public GameOver OverScene;

    public PauseMenu Pause;

    public GameObject StartMenu;
    


    void Awake()
    {
        //Call Sprites/Color in Resources
        puzzles = Resources.LoadAll<Sprite>("Match_2/Sprites/money");
        AudioSource = GetComponent<AudioSource>();
        Time.timeScale = 0f;

    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void StartGame()
    {
        GetButtons();
        AddListeners();
        AddGamePuzzles();
        Shuffle(gamePuzzles);
        Getbgimage();
        gameGuesses = gamePuzzles.Count / 2;
        Time.timeScale = 1f;
        StartMenu.SetActive(false);
    }

    public void go_menu()
    {
        SceneManager.LoadScene("Select");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickCount++;
            if (!IsInvoking("DoubleClick"))
                Invoke("DoubleClick", 1.0f);

        }
        else if (ClickCount == 2)
        {
            CancelInvoke("DoubleClick");
            SceneManager.LoadScene("Select");

        }

        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Home))
            {

            }

            else if (Input.GetKey(KeyCode.Escape))
            {
               
                Pause.Pause();
            }

            else if (Input.GetKey(KeyCode.Menu))
            {
          
            }
        }


        UIPoint.text = (totalPoint).ToString();
       
        if(LimitTime > 0)
        {
            LimitTime -= Time.deltaTime;
        }

        else if(LimitTime <= 0)
        {
            GameOver();
        }

        
        text_Timer.text = " " + Mathf.Round(LimitTime);
    }

    void DoubleClick()
    {
        ClickCount = 0;
    }

    void Playsound(string action)
    {
        switch (action)
        {
            case "Chose":
                AudioSource.clip = chosebtns;
                AudioSource.Play();
                break;
            case "Correct":
                AudioSource.clip = correctbtns;
                AudioSource.Play();
                break;
            case "Incorrect":
                AudioSource.clip = incorrectbtns;
                AudioSource.Play();
                break;
            case "Cancel":
                AudioSource.clip = cancelbtns;
                AudioSource.Play();
                break;
        }

    }

    //
    void GetButtons()
    {
        //tag name == PuzzleButton
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");


        for(int i=0; i<objects.Length; i++)
        {
            //Button 속성 부여
            btns.Add(objects[i].GetComponent<Button>());
            
        }
    }
    
    //시작시 색상부여
    void Getbgimage()
    {
        for(int i=0; i<btns.Count; i++)
        {
            btns[i].image.sprite = gamePuzzles[i];
        }
    }


    void AddGamePuzzles()
    {
        //총 버튼 수
        int looper = btns.Count;
        int index = 0;

        /*for(int i=0; i<looper; i++)
        {   
            // looper = 8, index == 4
            if(index == looper / 2)
            {
                index = 0;
            }

            //puzzles(Sprite form List) add Sprite in gamepuzzles that load Resources
            //총 수량의 절반만큼 색상부여
            //gamepuzzles은 스프라이트 형식의 리스트
            gamePuzzles.Add(puzzles[index]);
            index++;
        }*/

        for (int i = 0; i < looper; i++)
        {
            if(index == 4)
            {
                index = 0;
            }
            //puzzles(Sprite form List) add Sprite in gamepuzzles that load Resources
            //총 수량의 절반만큼 색상부여
            //gamepuzzles은 스프라이트 형식의 리스트
            gamePuzzles.Add(puzzles[index]);
            index++;
        }
    }

    void AddListeners()
    {
        foreach(Button btn in btns)
        {
            //each Button has PickPuzzle Listener
            btn.onClick.AddListener(() => PickPuzzle());
        }
    }


    public void PickPuzzle()
    {
        //clickButton
        //bring name
        string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        
        
        //if (!firstGuess) => firstGuess 가 거짓이면 작동 => firstGuess == false
        if (!firstGuess)
        {
            firstGuess = true;
            //사용자가 누른 버튼의 이름을 int로 받음
            firstGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            
            //사용자가 누른 버튼의 번호의 대한 gamePuzzles[i]번 인덱스의 이름
            firstGuessPuzzle = gamePuzzles[firstGuessIndex].name;
            
            

            //버튼 이미지 스프라이트는 게임퍼즐 인
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            btns[firstGuessIndex].image.color = new Color(1, 1, 1, .7f);
            Playsound("Chose");
        }

        else if (!secondGuess)
        {
            secondGuess = true;
            secondGuessIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondGuessPuzzle = gamePuzzles[secondGuessIndex].name;

            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            countGuesses++;

            //첫번째와 같은걸 골랐을 경우
            if (firstGuessIndex == secondGuessIndex)
            {
                //버튼 1,2 다시 뒷면으로
                btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
                btns[firstGuessIndex].image.color = new Color(1, 1, 1, 1);
                firstGuess = secondGuess = false;
                Playsound("Cancel");
            }

            else
            {
                CheckIfThePuzzlesMatch();
            }
        }
    }


    public void CheckIfThePuzzlesMatch()
    {
        //yield return new WaitForSeconds(1f);

        //만일 색상이 같아면
        if(firstGuessPuzzle == secondGuessPuzzle)
        {
            //0.5초동안 잠시 멈춘다
            //yield return new WaitForSeconds(.5f);

            //버튼1,2 각 다 비활성
            /*btns[firstGuessIndex].interactable = false;
            btns[secondGuessIndex].interactable = false;

            //버튼1,2 각 색상 제거
            btns[firstGuessIndex].image.color = new Color(0, 0, 0, 0);
            btns[secondGuessIndex].image.color = new Color(0, 0, 0, 0);
            */

            //이전이미지
            string temp = gamePuzzles[firstGuessIndex].name;
            string temp2 = gamePuzzles[secondGuessIndex].name;

            
            //새로운 이미지 부여
            int num = rand.Next(0,4); //0~3
            int num2 = rand.Next(0, 4);

            gamePuzzles[firstGuessIndex] = puzzles[num]; //Money_fin2
            gamePuzzles[secondGuessIndex] = puzzles[num2];

            //첫번째가 다시 같은 그림을 그릴때
            if (temp == gamePuzzles[firstGuessIndex].name)
            {
                while (true)
                {
                    num = rand.Next(0, 4);
                    gamePuzzles[firstGuessIndex] = puzzles[num];

                    if(temp != gamePuzzles[firstGuessIndex].name)
                    {
                        btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
                        btns[firstGuessIndex].image.color = new Color(1, 1, 1, 1);
                        break;
                    }
                }
            }

            else
            {
                gamePuzzles[firstGuessIndex] = puzzles[num];
                btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
                btns[firstGuessIndex].image.color = new Color(1, 1, 1, 1);
            }


            //2
            if (temp2 == gamePuzzles[secondGuessIndex].name)
            {
                while (true)
                {
                    num2 = rand.Next(0, 4);
                    gamePuzzles[secondGuessIndex] = puzzles[num2];

                    if (temp2 != gamePuzzles[secondGuessIndex].name)
                    {
                        btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
                        break;
                    }
                }
            }

            else
            {
                gamePuzzles[secondGuessIndex] = puzzles[num2];
                btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];
            }
            

            Playsound("Correct");

            if(temp == "Money_fin_0")
            {
                totalPoint = totalPoint + 500;
            }

            if(temp == "Money_fin_1")
            {
                totalPoint = totalPoint + 1000;
            }

            if(temp == "Money_fin_2")
            {
                totalPoint = totalPoint + 1500;
            }

            if(temp == "Money_fin_3")
            {
                totalPoint = totalPoint + 300;
            }

            
        }

        //not match
        else
        {   
            //동일하게 0.5초 멈추고
            //yield return new WaitForSeconds(.5f);

            //버튼 1,2 다시 뒷면으로
            btns[firstGuessIndex].image.sprite = gamePuzzles[firstGuessIndex];
            btns[secondGuessIndex].image.sprite = gamePuzzles[secondGuessIndex];

            btns[firstGuessIndex].image.color = new Color(1, 1, 1, 1);
            Playsound("Incorrect");
        }
        
        firstGuess = secondGuess = false;
    }

    //2개 색이 같다면
    /*void CheckIfTheGameIsFinished()
    {
        countCorrectGuesses++;

        /*if(countCorrectGuesses == gameGuesses)
        {
            Debug.Log("Game Finished");
            Debug.Log("It took you " + countGuesses + "many guess(es) to finish the game");
        }
    }*/

    void Shuffle(List<Sprite> list)
    {
        //리스트에는 4가지 색들어있음
        for(int i=0; i<list.Count; i++)
        {
            //리스트 0번을 temp
            Sprite temp = list[i];

            //0~4
            int randomIndex = Random.Range(i, list.Count);
            
            //리스트0 번은 리스트 랜덤이 들어감
            list[i] = list[randomIndex];

            //리스트 랜덤에는 temp가
            list[randomIndex] = temp;
        }
    }

    public void GameOver()
    {
        OverScene.Setup(totalPoint);
        AudioSource.Stop();
    }


    

}
