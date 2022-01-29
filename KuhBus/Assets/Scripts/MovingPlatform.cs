using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float speed = 1.0f;

    [SerializeField] private Vector2 movement;

    private float distance;
    private Vector2 startPosition;

    void Awake()
    {
        startPosition = transform.position;
        distance = movement.magnitude;
        movement.Normalize();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        //var dir = new Vector3(maxX - minX, maxY - minY, 0);
        //dir.Normalize();
        //if ((horizontal && (transform.position.x > maxX || transform.position.x < minX)) || (vertical && (transform.position.y > maxY || transform.position.y < minY)))
        //    speed *= -1.0f;
        //transform.position += dir * speed;


        transform.position = startPosition + distance * movement * Mathf.Sin(Time.time * speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  void OnDrawGizmos()
  {
        //Gizmos.DrawLine(new Vector3( (horizontal?minX:transform.position.x) , (vertical ? minY : transform.position.y), 0.0f), new Vector3((horizontal ? maxX : transform.position.x), (vertical ? maxY : transform.position.y), 0.0f));
        Vector2 start = startPosition - distance * movement;
        Vector2 end   = startPosition + distance * movement;

        Gizmos.DrawLine(start, end);
    }
}
