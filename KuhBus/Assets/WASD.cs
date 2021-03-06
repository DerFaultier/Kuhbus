using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASD : MonoBehaviour
{
  public float Speed = 1.0f;
  public float RotationSpeed = 1.0f;
  public float BackwardsDrive = 1.0f;

  public float multiplier = 1.0f;


  // Start is called before the first frame update
  void Start()
  {
    
  }

  // Update is called once per frame
  void FixedUpdate()
  {

    bool w = Input.GetKey(KeyCode.W);
    bool a = Input.GetKey(KeyCode.A);
    bool s = Input.GetKey(KeyCode.S);
    bool d = Input.GetKey(KeyCode.D);

    float xSpeed = -((a ? -1.0f : 0) + (d ? 1.0f : 0)) * RotationSpeed * multiplier;
    float ySpeed = (s ? -1.0f : 0) * BackwardsDrive * multiplier + (w ? 1.0f : 0) * Speed * multiplier;

    transform.GetComponent<Rigidbody2D>().angularVelocity = xSpeed;
    float angle = Mathf.PI * transform.GetComponent<Rigidbody2D>().rotation / 180.0f;
    transform.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sin(-angle) * ySpeed, Mathf.Cos(-angle) * ySpeed);
  }

  
}
