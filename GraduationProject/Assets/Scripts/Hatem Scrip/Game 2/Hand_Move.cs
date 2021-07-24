using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand_Move : MonoBehaviour
{
    Rigidbody cube;
    public static int speed ;
    public static bool cube_stop = false;  
  //  RecievedData DataReciever;
    
    void Start()
    {
       
      //  DataReciever = FindObjectOfType<RecievedData>();
        cube = this.GetComponent<Rigidbody>();

    }


    void Update()
    {
       // move = DataReciever.gesture;
        
        

    }
  
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "End")
        {
            Destroy(this.gameObject);

        }
    }
    private void FixedUpdate()
    {
        // cube.AddForce(0, -4, 0 ,  ForceMode.Acceleration); 
        if (!cube_stop)
        {
            cube.velocity = new Vector3(0, speed, 0);
        }
        else
        {
            cube.velocity = new Vector3(0, 0, 0);
        }
       
    }
 

    private void OnMouseDown()
    {
        Destroy(this.gameObject);
        Game_Leader2.points++; 
    }

    public void get_move(Button button)
    {  
     //   move = button.name;
      //  Destroy(GameObject.Find(move));
       // Destroy(GameObject.FindGameObjectsWithTag(move)[0]);
    }
}
