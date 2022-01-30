using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
