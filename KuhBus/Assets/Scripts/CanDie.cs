using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanDie : MonoBehaviour
{

    [SerializeField] private int HP = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void receiveDmg(int amount)
    {
        HP -= amount;
        if (HP == 0)
            Destroy(gameObject);
    }
}
