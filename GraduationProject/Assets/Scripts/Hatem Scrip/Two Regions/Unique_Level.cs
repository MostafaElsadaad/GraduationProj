using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unique_Level : MonoBehaviour
{
    public const float limit = 1.0f , BlueSideX_Min = -2.79f, BlueSideX_Max = 1.64f, RedSideX_Min = 3.67f, RedSideX_Max = 8.14f, Y = 0.83f, Z_Min = -4.43f  , Z_Max = 4.6f ; // playground_coordinates
   
    public Vector3[] RedSide_Cubes = new Vector3 [10] , BlueSide_Cubes = new Vector3[10];
    public GameObject RedCube_prefab, BlueCube_prefab;
    public static int BlueCube_Num = 0 , RedCube_Num = 0 , Spwaned_Cubes = 0  ;
   
    
    public struct Area { public float Xmax ;  public float Xmin;} ;
    Area RedArea, BlueArea;
   
    void Start()
    {

       
        RedArea.Xmax = RedSideX_Max; RedArea.Xmin = RedSideX_Min; 
        BlueArea.Xmax = BlueSideX_Max; BlueArea.Xmin = BlueSideX_Min;

         Cube_Spwan(RedCube_prefab, 3, BlueArea, BlueSide_Cubes );
         Cube_Spwan(RedCube_prefab, 3, RedArea, RedSide_Cubes);

        // Cube_Spwan(BlueCube_prefab, 3, BlueArea , BlueSide_Cubes);
         //Cube_Spwan(BlueCube_prefab, 3, RedArea, RedSide_Cubes);


    }
        
    
    private void Cube_Spwan( GameObject prefab , int number , Area area , Vector3[] Cubes  )
        
    {
        string AreaName;
        int Cube_Num ;
        if (area.Xmax == BlueSideX_Max) { Cube_Num = BlueCube_Num; AreaName = "Blue"; }
        else { Cube_Num = RedCube_Num; AreaName = "Red"; } 

            if (Cube_Num < 10)
        {
           
            for (int i = 0; i < number; i++) {
                Cubes[Cube_Num] = new Vector3(Random.Range(area.Xmin, area.Xmax), Y, Random.Range(Z_Min, Z_Max));
                Distance_Check(Cube_Num, area, Cubes);
                StartCoroutine(Generate_Cube(0.7f, prefab , Cubes , Cube_Num));
                Cube_Num++;
             
                Spwaned_Cubes++; 
            }
            if (AreaName == "Blue") BlueCube_Num = Cube_Num;
            else RedCube_Num = Cube_Num;



        }
     

    }

    IEnumerator Generate_Cube( float waitTime , GameObject prefab , Vector3 [] Cubes , int num)
    {
        GameObject SpwanedCube = Instantiate(prefab, Cubes[num], new Quaternion(0, 0, 0, 1));
       
        yield return new WaitForSeconds(waitTime);
    }
    private void Distance_Check( int cube_num ,  Area area , Vector3 [] Cubes)
    {
        float distanceX, distanceZ;
        string flag; 

        if( cube_num > 0)
        {
            for(int cnt =1; cnt < (cube_num+1); cnt++) 
            {   
                if(Cubes[cube_num].z >= Cubes[cube_num - cnt].z)
                {
                    distanceZ = Cubes[cube_num].z - Cubes[cube_num - cnt].z; 

                }
                else
                {
                    distanceZ = Cubes[cube_num - cnt].z - Cubes[cube_num].z;

                }
                if( distanceZ < limit)
                {
                     
                    if (Cubes[cube_num].x >= Cubes[cube_num - cnt].x)
                    {
                        distanceX = Cubes[cube_num].x - Cubes[cube_num - cnt].x;
                        flag = "greater_than";
                        Debug.Log("greaterthanZ" + cube_num + " " + (cube_num-cnt) + " "+ area.Xmax );
                    }
                    else
                    {
                        distanceX =  Cubes[cube_num - cnt].x - Cubes[cube_num].x ;
                        flag = "less_than";
                        Debug.Log("lessthanZ" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);
                    }
                    if( distanceX < limit)
                    {
                        if ( flag == "greater_than")
                        {
                            if (Cubes[cube_num].x + (limit - distanceX) < area.Xmax) Cubes[cube_num].x += (limit - distanceX); 
                            else Cubes[cube_num].x -= (limit + distanceX);
                            Debug.Log("greaterthanX" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);
                        }
                        else
                        {
                            if (Cubes[cube_num].x - (limit - distanceX) > area.Xmin) Cubes[cube_num].x -= (limit - distanceX);
                            else Cubes[cube_num].x += (limit + distanceX);
                            Debug.Log("lessthanX" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);

                        }
                    }
                }
          
                }
            }
        } 
      
            
    }



 
