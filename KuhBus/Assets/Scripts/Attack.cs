using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float range=1.0f;

    public bool attacking = false;

    public AudioSource attackSoundSource;
    public List<AudioClip> attackSounds;
    public AudioClip kopfnussSound;
    int soundIdx = 0;

    float attackDelay = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay <= 0)
        {
            attacking = false; 
            attackDelay = 0;
        }
    }

    public void doAttack(int dmg)
    {
        if (attacking) return;
        attackDelay = 0.5f;
        attacking = true;

        if (dmg > 0)
        {
            attackSoundSource.clip = attackSounds[soundIdx++];
            if (soundIdx > attackSounds.Count) soundIdx = 0;
        }
        else
        {
            attackSoundSource.clip = kopfnussSound;
        }

        attackSoundSource.Play();

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, new Vector2(1.0f*transform.localScale.x, 0.0f), range);
        if (hit.Length>2)
        {
            var dmgDeal = hit[2].transform.GetComponent<CanDie>();
            if (dmgDeal)
                dmgDeal.receiveDmg(dmg);
        }
    }
}
