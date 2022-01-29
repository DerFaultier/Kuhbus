using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
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
        //GetComponent("GameOverPage").gameObject.SetActive(true);
        //transform.GetChild(0).gameObject.SetActive(false);
        transform.Find("Lifebar").gameObject.SetActive(false);
        transform.Find("GameOverPage").gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        Player.OnPlayerDied += OnPlayerDied_Func;
    }

    private void OnDisable()
    {
        Player.OnPlayerDied -= OnPlayerDied_Func;
    }
}
