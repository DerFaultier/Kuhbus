using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealsDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    if(collision.collider.tag=="Player")
    {
      print("Kuhbus recaived damage!!!");
      var contactNormal = collision.contacts[0].normal;
      collision.collider.GetComponent<PlayerController>().stun(0.2f);
      collision.collider.GetComponent<Rigidbody2D>().velocity=contactNormal * -20;  //.AddForce(contactNormal*5, ForceMode2D.);
    }
  }
}
