using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private bool horizontal;
    [SerializeField] private bool vertical;

    [SerializeField] private float speed = 1.0f;


  // Start is called before the first frame update
    void Start()
    {
        if(!horizontal)
        {
            minX = transform.position.x;
            maxX = transform.position.x;
        }
        if (!vertical)
        {
            minX = transform.position.y;
            maxY = transform.position.y;
        }
    }

    private void FixedUpdate()
    {
        var dir = new Vector3(maxX - minX, maxY - minY, 0);
        dir.Normalize();
        if ((horizontal && (transform.position.x > maxX || transform.position.x < minX)) || (vertical && (transform.position.y > maxY || transform.position.y < minY)))
            speed *= -1.0f;
        transform.position += dir * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  void OnDrawGizmos()
  {
    Gizmos.DrawLine(new Vector3( (horizontal?minX:transform.position.x) , (vertical ? minY : transform.position.y), 0.0f), new Vector3((horizontal ? maxX : transform.position.x), (vertical ? maxY : transform.position.y), 0.0f));
  }
}
