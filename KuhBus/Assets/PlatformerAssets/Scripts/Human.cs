using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Human : MonoBehaviour
{
    private enum State { idle, walk_left, walk_right};
    private State state = State.idle;
    private Animator anim;
    private float secTotal;
    public float walkSpeed = 1;
    public float idleTime = 3;
    public float walkLeftTime = 5;
    public float walkRightTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        secTotal = idleTime + walkLeftTime + walkRightTime;
    }

    // Update is called once per frame
    void Update()
    {
        float secs = Time.realtimeSinceStartup % secTotal;
        if (secs <= idleTime)
        {
            state = State.idle;
        }
        else if(secs <= idleTime + walkLeftTime)
        {
            state = State.walk_left;
            transform.Translate(-walkSpeed/1000, 0, 0);
        }
        else
        {
            state = State.walk_right;
            transform.Translate(walkSpeed/1000, 0, 0);
        }

        anim.SetInteger("state", (int)state);
    }
}
