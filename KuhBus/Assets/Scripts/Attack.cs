using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float range=1.0f;
    public int dmg=1;

    public bool attacking = false;

    public AudioSource attackSoundSource;
    public List<AudioClip> attackSounds;
    int soundIdx = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doAttack()
    {
        if (attacking) return;
        attacking = true;

        attackSoundSource.clip = attackSounds[soundIdx++];
        if (soundIdx > attackSounds.Count) soundIdx = 0;
        attackSoundSource.Play();

        StartCoroutine("stopAttack", 0.5f);
        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, new Vector2(1.0f*transform.localScale.x, 0.0f), range);
        if (hit.Length>2)
        {
            var dmgDeal = hit[2].transform.GetComponent<CanDie>();
            if (dmgDeal)
                dmgDeal.receiveDmg(dmg);
        }
    }

    public IEnumerator stopAttack(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        attacking = false;
    }
}
