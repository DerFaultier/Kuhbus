using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int max_life = 100;
    public int life = 100;
    private LifeBar lifebar;
    public bool catEvil = false;
    public float catEvilProbability = 0.5f;
    public float minTimeToUpdate = 5.0f;    // minimum time until cat might change to evil
    //public float updateTime = 2.5f;     // time in seconds to check if cat changes to evil after minTimeToUpdate

    private float timer = 0.0f;
    private System.Random rnd;
    private int precision = 1000;    // precision of random number
    private bool game_started = false;

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnDamageReceived;
    public static event PlayerDelegate OnPlayerHealed;
    public static event PlayerDelegate OnPlayerWon;

    // Start is called before the first frame update
    void Start()
    {
        lifebar = Singleton.instance.lifebar;
        timer = 0.0f;
        rnd = new System.Random();
    }

    void OnGameStarted_Func()
    {
        timer = 0.0f;
        game_started = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= minTimeToUpdate && game_started)
        {
            int r = rnd.Next(0, precision);
            if (catEvil == false)
            {
                if (r >= catEvilProbability * precision)
                {
                    // cat turns evil
                    catEvil = true;
                }
            }
            else
            {
                if (r < catEvilProbability * precision)
                {
                    // cat turns good
                    catEvil = false;
                }
            }
            timer = 0;
        }
    }

    public void healPlayer(int heal)
    {
        life = Mathf.Min(life + heal, max_life);
        lifebar.setLife((float)life / max_life);
        if (heal > 0)
            OnPlayerHealed();
    }

    public void hurtPlayer(int damage)
    {
        life = Mathf.Max(life - damage, 0);
        lifebar.setLife((float)life / max_life);

        if (life <= 0)
            OnPlayerDied();
        else
            OnDamageReceived();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "InstantDeath")
            OnPlayerDied();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "GameWon")
        {
            OnPlayerWon();
            Debug.Log("Game Won");
        }
    }

    private void OnEnable()
    {
        GameStates.OnGameStarted += OnGameStarted_Func;
    }

    private void OnDisable()
    {
        GameStates.OnGameStarted -= OnGameStarted_Func;
    }
}
