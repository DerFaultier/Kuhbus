using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(player))]
public class Heals : MonoBehaviour
{
    public bool toEvilCat=true;
    public bool toGoodCat=true;
    public int heal = 2;
    public float healing_time = 0.5f;
    private bool in_heal_zone = false;
    private float time_counter = 0.0f;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (in_heal_zone)
        {
            if (time_counter >= healing_time)
            {
                //GetComponent<Player>().healPlayer(heal);
                print("Kuhbus got healed!!!");
                player.healPlayer(heal);
                time_counter = 0;
            }
            time_counter += Time.deltaTime;
        }
    }

    bool CatCondition(GameObject cat)
    {
        return (cat.GetComponent<Player>().catEvil && toEvilCat) || (!cat.GetComponent<Player>().catEvil && toGoodCat);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && CatCondition(collision.gameObject))
        {
            in_heal_zone = true;
            time_counter = healing_time + 1; // enforce direct heal
            player = collision.collider.GetComponent<Player>();
            player.transform.Find("HealParticles").gameObject.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && CatCondition(collision.gameObject))
        {
            time_counter = 0;
            in_heal_zone = false;
            player.transform.Find("HealParticles").gameObject.SetActive(false);
        }
    }
}
