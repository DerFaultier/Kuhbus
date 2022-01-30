using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStates : MonoBehaviour
{

    public float time_start_page_shown = 3;   // time in seconds to show start page
    private bool game_started = false;
    private bool game_won = false;
    private float timer = 0.0f;
    private GameObject gamepage;
    private GameObject ui_timer;
    private GameObject lifebar;
    private float highscore;


    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;

    // Start is called before the first frame update
    void Start()
    {
        gamepage = transform.Find("GamePage").gameObject;
        ui_timer = gamepage.transform.Find("Timer").gameObject;
        lifebar = gamepage.transform.Find("Lifebar").gameObject;
        gamepage.SetActive(false);
        transform.Find("GameStartPage").gameObject.SetActive(true);
        highscore = PlayerPrefs.GetFloat("HighScore");
        gamepage.transform.Find("Highscore").gameObject.GetComponent<Text>().text = "Highscore: " + highscore.ToString("0.00");
    }

    void FixedUpdate()
    {
        if (!game_started)
        {
            //Time.realtimeSinceStartup;
            if (timer >= time_start_page_shown)
            {
                gamepage.SetActive(true);
                transform.Find("GameStartPage").gameObject.SetActive(false);
                OnGameStarted();
                game_started = true;
                timer = 0.0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
        else if (!game_won)
        {
            timer += Time.deltaTime;
            ui_timer.GetComponent<Text>().text = "Time: " + timer.ToString("0.00");
        }
    }

    void OnPlayerDied_Func()
    {
        gamepage.gameObject.SetActive(false);
        transform.Find("GameOverPage").gameObject.SetActive(true);
    }

    void OnPlayerWon_Func()
    {
        game_won = true;
        transform.Find("GameWonPage").gameObject.SetActive(true);
        gamepage.gameObject.SetActive(true);
        lifebar.SetActive(false);
        if (highscore < Mathf.Epsilon || timer < highscore)
        {
            PlayerPrefs.SetFloat("HighScore", timer);
        }
        gamepage.transform.Find("Highscore").gameObject.GetComponent<Text>().text = "Highscore: " + PlayerPrefs.GetFloat("HighScore").ToString("0.00");
    }

    private void OnEnable()
    {
        Player.OnPlayerDied += OnPlayerDied_Func;
        Player.OnPlayerWon += OnPlayerWon_Func;
    }

    private void OnDisable()
    {
        Player.OnPlayerDied -= OnPlayerDied_Func;
        Player.OnPlayerWon -= OnPlayerWon_Func;
    }
}
