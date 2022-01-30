using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Human : MonoBehaviour
{
    private enum State { idle, walk};
    private State state = State.idle;
    private Animator anim;
    private float secTotal;
    private Vector2 scal;
    public float walkSpeed = 1;
    public float idleTime = 3;
    public float walkLeftTime = 5;
    public float walkRightTime = 5;
    private bool game_started = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        scal = transform.localScale;
        secTotal = idleTime + walkLeftTime + walkRightTime;
    }

    void OnGameStarted_Func()
    {
        game_started = true;
    }

    // Update is called once per frame
    void Update()
    {
        float secs = Time.realtimeSinceStartup % secTotal;
        if (secs <= idleTime || !game_started)  // human shall idle until game is started
        {
            state = State.idle;
        }
        else if(secs <= idleTime + walkLeftTime)
        {
            state = State.walk;
            transform.localScale = scal;
            transform.Translate(-walkSpeed*Time.deltaTime, 0, 0);
        }
        else
        {
            state = State.walk;
            transform.localScale = new Vector2(scal.x*(-1), scal.y);
            transform.Translate(walkSpeed * Time.deltaTime, 0, 0);
        }

        anim.SetInteger("state", (int)state);
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
