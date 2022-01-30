using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(player))]
public class DealsDamage : MonoBehaviour
{
    public bool toEvilCat=true;
    public bool toGoodCat=true;
    public int damage = 10;

    private AudioSource audioEffect;

    // Start is called before the first frame update
    void Start()
    {
        audioEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool CatCondition(GameObject cat)
    {
        return (cat.GetComponent<Player>().catEvil && toEvilCat) || (!cat.GetComponent<Player>().catEvil && toGoodCat);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && CatCondition(collision.gameObject))
        {
            print("Kuhbus received damage!!!");
            if (audioEffect) audioEffect.Play();
            collision.collider.GetComponent<Player>().hurtPlayer(damage);
            var contactNormal = collision.contacts[0].normal;
            collision.collider.GetComponent<PlayerController>().stun(0.2f);
            collision.collider.GetComponent<Rigidbody2D>().velocity = contactNormal * -20;  //.AddForce(contactNormal*5, ForceMode2D.);
        }
    }
}
