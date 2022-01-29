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

    private enum State { idle, run, jump, fall, evil_idle, evil_run, evil_jump, evil_fall };
    private State state = State.idle;
    private Rigidbody2D rb;
    private Collider2D  coll;
    private Animator    anim;
    private Player player;

    private float defaultGravity;
    private float v0;

    private bool isGrounded;
    private Transform platform;

    private bool isStunned;


  // Start is called before the first frame update
  void Start()
    {
        rb   = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();

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

        groundCheck();

        movement();
        animationState();
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, -rayLength, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }

    private void groundCheck() {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0,-1,0), rayLength, ground);
        isGrounded = hit;
        if (!hit) return;

        transform.parent = null;
        platform = null;
        if (hit && hit.transform.GetComponent<MovingPlatform>())
        {
            transform.parent = hit.transform;
            platform = hit.transform;
        }
    } 

    private void movement()
    {
        if (!isStunned)
        {
          float hMovement = Input.GetAxis("Horizontal");
          rb.velocity = new Vector2(hMovement * moveSpeed, rb.velocity.y);

          if (Input.GetButtonDown("Jump") && isGrounded)
          {
            rb.velocity = new Vector2(rb.velocity.x, v0);
            state = player.catEvil ? State.evil_jump : State.jump;
          }
        }

        if ( !isGrounded && ( !Input.GetButton("Jump") || rb.velocity.y < 0.1f))
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

        if (isGrounded)
        {
            if (Mathf.Abs(rb.velocity.x) > 0.1f)
                state = player.catEvil ? State.evil_run : State.run;
            else
                state = player.catEvil ? State.evil_idle : State.idle;
        } else
        {
            if (rb.velocity.y < 0.1f) state = player.catEvil ? State.evil_fall : State.fall;
            else                      state = player.catEvil ? State.evil_jump : State.jump;
        }

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

    void OnDamageReceived_Func()
    {
        //anim.SetInteger("state", (int)state);
    }

    void OnPlayerDied_Func()
    {
        rb.simulated = false;
    }

    private void OnEnable()
    {
        Player.OnDamageReceived += OnDamageReceived_Func;
        Player.OnPlayerDied += OnPlayerDied_Func;
    }

    private void OnDisable()
    {
        Player.OnDamageReceived -= OnDamageReceived_Func;
        Player.OnPlayerDied -= OnPlayerDied_Func;
    }
}



