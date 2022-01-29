using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public int max_life = 100;
    public int life = 100;
    private LifeBar lifebar;

    public delegate void PlayerDelegate();
    public static event PlayerDelegate OnPlayerDied;
    public static event PlayerDelegate OnDamageReceived;
    public static event PlayerDelegate OnPlayerHealed;

    // Start is called before the first frame update
    void Start()
    {
        lifebar = Singleton.instance.lifebar;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void healPlayer(int heal)
    {
        life = Mathf.Min(life + heal, max_life);
        lifebar.setLife((float) life / max_life);
        if (heal > 0)
            OnPlayerHealed();
    }

    public void hurtPlayer(int damage)
    {
        life = Mathf.Max(life - damage, 0);
        lifebar.setLife((float) life / max_life);

        if (life <= 0)
            OnPlayerDied();
        else
            OnDamageReceived();
    }

}
