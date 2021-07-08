using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand_Move : MonoBehaviour
{
    Rigidbody cube;
    RecievedData DataReciever;
    string move;
    void Start()
    {
        DataReciever = FindObjectOfType<RecievedData>();
        cube = this.GetComponent<Rigidbody>();

    }


    void Update()
    {
        move = DataReciever.gesture;
        Track_Move(move);
        Debug.Log(move);

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
        cube.velocity = new Vector3(0, -1, 0);
    }
    private void Track_Move(string move)
    {
        if (!string.IsNullOrEmpty(move) && !move.Equals("open") && !move.Equals("grasp"))
        {
            Destroy(GameObject.FindGameObjectsWithTag(move)[0]);
            Game_Leader2.points++;
        }
    }

    private void OnMouseDown()
    {
        Destroy(this.gameObject);
        Game_Leader2.points++; 
    }

    public void get_move(Button button)
    {  
        move = button.name;
        Destroy(GameObject.Find(move));
       // Destroy(GameObject.FindGameObjectsWithTag(move)[0]);
    }
}
