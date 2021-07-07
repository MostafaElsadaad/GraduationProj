using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class tt
{
  // public static  int y =0 ; 
}
public class Test : MonoBehaviour
{
    public GameObject kk;
   private Vector3 x = new Vector3(0 , 0.05f , 0);
    private Vector3 y = new Vector3(0, 0.025f, 0);

    void Start()
    {
       kk.GetComponent<Transform>().localScale += x ;
        kk.GetComponent<Transform>().position += y; 


    }

    // Update is called once per frame
    void Update()
    {
        kk.GetComponent<Transform>().localScale += x;
        kk.GetComponent<Transform>().position += y;
    }
 
}
