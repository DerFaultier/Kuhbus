using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{

    public float time_start_page_shown = 3;   // time in seconds to show start page
    private bool game_started = false;
    private float timer = 0.0f;

    public delegate void GameDelegate();
    public static event GameDelegate OnGameStarted;

    // Start is called before the first frame update
    void Start()
    {
        transform.Find("Lifebar").gameObject.SetActive(false);
        transform.Find("GameStartPage").gameObject.SetActive(true);
    }

    void FixedUpdate()
    {
        if (!game_started)
        {
            //Time.realtimeSinceStartup;
            if (timer >= time_start_page_shown)
            {
                transform.Find("Lifebar").gameObject.SetActive(true);
                transform.Find("GameStartPage").gameObject.SetActive(false);
                OnGameStarted();
                game_started = true;
            }
            timer += Time.deltaTime;
        }
    }

    void OnPlayerDied_Func()
    {
        transform.Find("Lifebar").gameObject.SetActive(false);
        transform.Find("GameOverPage").gameObject.SetActive(true);
    }

    void OnPlayerWon_Func()
    {
        transform.Find("Lifebar").gameObject.SetActive(false);
        transform.Find("GameWonPage").gameObject.SetActive(true);
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
