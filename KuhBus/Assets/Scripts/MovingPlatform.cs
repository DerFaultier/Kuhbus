using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float speed = 1.0f;

    [SerializeField] private Vector2 movement;

    private Vector2 startPosition;
    private bool awake = false;

    void Awake()
    {
        awake = true;
        startPosition = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        transform.position = startPosition + movement * Mathf.Sin(Time.time * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //[ExecuteInEditMode]
    void OnDrawGizmos()
  {
        if (!awake)
          Awake();

        Vector2 start = startPosition - movement;
        Vector2 end   = startPosition + movement;

        Gizmos.DrawLine(start, end);
    }
}
