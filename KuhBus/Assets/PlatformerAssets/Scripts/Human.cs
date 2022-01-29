using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Human : MonoBehaviour
{
    private enum State { idle, walk_left, walk_right};
    private State state = State.idle;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("state", (int)state);
    }
}
