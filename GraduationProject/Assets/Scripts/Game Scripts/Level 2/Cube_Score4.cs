using System.Collections;
using UnityEngine;
public class Cube_Score4 : MonoBehaviour
{

    private string Object_Tag, Collider_Tag;
    public bool Scored = false, moved = false;
    private float boundryX_Min, boundryX_Max, boundryZ_Min, boundryZ_Max;
    private Vector3 First_Position, Current_Position ;
   
    private void Start()
    {
        First_Position = this.GetComponent<Rigidbody>().position;
        Object_Tag = this.gameObject.tag;
        switch (Object_Tag)
        {
            case "Green_Cube":
                Collider_Tag = "Green_Side";
                boundryX_Min = Level_2_Generation.GreenSideX_Min;
                boundryX_Max = Level_2_Generation.GreenSideX_Max;
                boundryZ_Min = Level_2_Generation.GreenSideZ_Min;
                boundryZ_Max = Level_2_Generation.GreenSideZ_Max;
                break;
            case "Purple_Cube":
                Collider_Tag = "Purple_Side";
                boundryX_Min = Level_2_Generation.PurpleSideX_Min;
                boundryX_Max = Level_2_Generation.PurpleSideX_Max;
                boundryZ_Min = Level_2_Generation.PurpleSideZ_Min;
                boundryZ_Max = Level_2_Generation.PurpleSideZ_Max;
                break;
            case "Orange_Cube":
                Collider_Tag = "Orange_Side";
                boundryX_Min = Level_2_Generation.OrangeSideX_Min;
                boundryX_Max = Level_2_Generation.OrangeSideX_Max;
                boundryZ_Min = Level_2_Generation.OrangeSideZ_Min;
                boundryZ_Max = Level_2_Generation.OrangeSideZ_Max;
                break;
            case "Sky_Cube":
                Collider_Tag = "Sky_Side";
                boundryX_Min = Level_2_Generation.SkySideX_Min;
                boundryX_Max = Level_2_Generation.SkySideX_Max;
                boundryZ_Min = Level_2_Generation.SkySideZ_Min;
                boundryZ_Max = Level_2_Generation.SkySideZ_Max;
                break;

        }
    }
    private void Update()
    {
        Current_Position = this.GetComponent<Rigidbody>().position;
    }

    private void OnCollisionEnter(Collision collision)
    {
       


        if (First_Position.x != Current_Position.x || First_Position.z != Current_Position.z) moved = true;

        if (Scored == false && moved == true)
        {

            if (collision.gameObject.tag == Collider_Tag)
            {
                Scored = true;
                StartCoroutine(One_Point(1.0f));

            }

            else if (collision.gameObject.tag == "Green_Cube" || collision.gameObject.tag == "Purple_Cube" || collision.gameObject.tag == "Orange_Cube" || collision.gameObject.tag == "Sky_Cube")
            {

                if (collision.gameObject.GetComponent<Transform>().position.x <= boundryX_Max && collision.gameObject.GetComponent<Transform>().position.x >= boundryX_Min
                    && collision.gameObject.GetComponent<Transform>().position.z <= boundryZ_Max && collision.gameObject.GetComponent<Transform>().position.z >= boundryZ_Min)

                {
                    Scored = true;
                    StartCoroutine(One_Point(1.0f));

                }
            }
        }



    }

    IEnumerator One_Point(float waitTime)
    {
        Game_Leader.points++;
        yield return new WaitForSeconds(waitTime);

    }
}
