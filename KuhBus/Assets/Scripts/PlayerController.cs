using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float jumpDistance = 3f;
    [SerializeField] private float fallingGravity = 3f;
    [SerializeField] private LayerMask ground;
    [SerializeField] private float rayLength = 1f;

    private enum State { idle, run, jump, fall };
    private State state = State.idle;
    private Rigidbody2D rb;
    private Collider2D  coll;
    private Animator    anim;

    private float defaultGravity;
    private float v0;

   private bool isStunned;

  // Start is called before the first frame update
  void Start()
    {
        rb   = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();


        v0 = 2f * jumpHeight * moveSpeed / jumpDistance;
        rb.gravityScale = 2f * jumpHeight * (moveSpeed * moveSpeed) / (jumpDistance * jumpDistance);

        defaultGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        v0              = 2f * jumpHeight * moveSpeed / jumpDistance;
        rb.gravityScale = 2f * jumpHeight * (moveSpeed * moveSpeed) / (jumpDistance * jumpDistance);

        defaultGravity = rb.gravityScale;

        movement();
        animationState();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -rayLength, 0));
    }

    private void nothing()
    {

    }
    private bool IsGrounded() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0,-1,0), rayLength, ground);
        return /*coll.IsTouchingLayers(ground) ||*/ hit;
    } 

    private void movement()
    {
        if (!isStunned)
        {
          float hMovement = Input.GetAxis("Horizontal");
          rb.velocity = new Vector2(hMovement * moveSpeed, rb.velocity.y);

          if (Input.GetButtonDown("Jump") && IsGrounded())
          {
            rb.velocity = new Vector2(rb.velocity.x, v0);
            state = State.jump;
          }
        }

        if ( !IsGrounded() && ( !Input.GetButton("Jump") || rb.velocity.y < 0.1f))
        {
            rb.gravityScale = fallingGravity * defaultGravity;
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }

        if (rb.velocity.x < -Mathf.Epsilon)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (rb.velocity.x > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void animationState()
    {

        if (IsGrounded())
        {
            if (Mathf.Abs(rb.velocity.x) > 0.1f)
                state = State.run;
            else
                state = State.idle;
        } else
        {
            if (rb.velocity.y < 0.1f) state = State.fall;
            else                      state = State.jump;
        }

        //switch (state)
        //{
        //    default:
        //    case State.idle:
        //    case State.run:
        //        if (rb.velocity.y > 0.1f && !IsGrounded())
        //            state = State.jump;
        //        else if (Mathf.Abs(rb.velocity.x) > 0.1f) 
        //            state = State.run;
        //        else 
        //            state = State.idle;
        //        break;
        //    case State.jump:
        //        if (rb.velocity.y < 0) state = State.fall;  break;
        //    case State.fall:
        //        if (coll.IsTouchingLayers(ground)) state = State.idle; break;
        //}

        anim.SetInteger("state", (int)state); 
    }


    public void stun(float seconds)
    {
      StopAllCoroutines();
      isStunned = true;
      StartCoroutine("stopStun", seconds);
    }

    private IEnumerator stopStun(float seconds)
    {
      yield return new WaitForSeconds(seconds);
      isStunned = false;
    }
}



