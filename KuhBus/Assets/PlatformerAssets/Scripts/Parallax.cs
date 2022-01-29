using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float factor = 0.5f;
    public int offset = 0;
    public int width = 20;
    [SerializeField] private new Transform camera;

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t = transform.position;
        float x = camera.position.x * factor;
        float delta = 0;
        if (x>0)
          delta = Mathf.FloorToInt( (camera.position.x - x) / width ) * width + offset;
        else
          delta = Mathf.CeilToInt( (camera.position.x - x) / width ) * width + offset;

    t[0] = x + delta;

        t[1] = Mathf.Clamp(camera.position.y * factor, -5, +5);
        transform.position = t;
    }
}
