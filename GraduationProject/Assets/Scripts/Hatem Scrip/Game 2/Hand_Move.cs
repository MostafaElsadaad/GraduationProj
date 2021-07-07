using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Move : MonoBehaviour
{
    Rigidbody cube; 
    void Start()
    {
        cube = this.GetComponent<Rigidbody>();
       
    }

   
    void Update()
    {
      
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "End")
        {
            Destroy(this.gameObject);
           
        }
    }
    private void FixedUpdate()
    {
        // cube.AddForce(0, -4, 0 ,  ForceMode.Acceleration); 
        cube.velocity = new Vector3(0, -1, 0);
    }
    private void OnMouseDown()
    {
        Destroy(this.gameObject);
        Game_Leader2.points++; 
    }
}
