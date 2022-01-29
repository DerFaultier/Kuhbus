using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float range=1.0f;
    public int dmg=1;

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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector3(0, -1, 0), range);
        if (hit)
        {
            var dmgDeal = hit.transform.GetComponent<CanDie>();
            if (dmgDeal)
                dmgDeal.receiveDmg(dmg);
        }
    }
}
