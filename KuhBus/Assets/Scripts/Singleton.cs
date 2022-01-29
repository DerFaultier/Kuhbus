using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Singleton : MonoBehaviour
{
  public static Singleton instance;

  public LifeBar lifebar;

  // Start is called before the first frame update
  public Singleton() 
  {
    instance = this;
  }

  // Update is called once per frame
  void Update()
  {

  }
}
