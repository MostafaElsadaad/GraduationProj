using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Cube_Score : MonoBehaviour
{

    private string Object_Tag, Collider_Tag;
    public bool Scored = false , moved = false; 
    private float timer = 0.0f, boundryMin, boundryMax;
    private Vector3 First_Position, Current_Position;

    private void Start()
    {
        First_Position = this.GetComponent<Rigidbody>().position;
        Object_Tag = this.gameObject.tag;
       
        switch (Object_Tag)
        {
            case "Blue_Cube":
                Collider_Tag = "Blue_Side";
                boundryMin = Level_1_Generation.BlueSideX_Min;
                boundryMax = Level_1_Generation.BlueSideX_Max;
                
                break;
            case "Red_Cube":
                Collider_Tag = "Red_Side";
                boundryMin = Level_1_Generation.RedSideX_Min;
                boundryMax = Level_1_Generation.RedSideX_Max;
                break;
           
        }
        
    }
    private void Update()
    {
        Current_Position = this.GetComponent<Rigidbody>().position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Cube_In_Score())
        {
            if (First_Position.x != Current_Position.x || First_Position.z != Current_Position.z) moved = true;
            if (Scored == false && moved == true)
            {
                if (collision.gameObject.tag == Collider_Tag)
                {

                    One_Point();
                    Scored = true;

                }
                if (collision.gameObject.tag == "Blue_Cube" || collision.gameObject.tag == "Red_Cube")
                {

                    if (collision.gameObject.GetComponent<Transform>().position.x <= boundryMax && collision.gameObject.GetComponent<Transform>().position.x >= boundryMin)
                    {
                        One_Point();
                        Scored = true;

                    }
                }

            }
        }
     
    }
    private void One_Point()
    {
        if ((Time.time - timer) > 1)
        {
            Game_Leader.points++;
        }
        timer = Time.time;
    }
    private bool Cube_In_Score()
    {
        if (First_Position.x <= boundryMax && First_Position.x >= boundryMin) return false;
        else return true;
    }
}
