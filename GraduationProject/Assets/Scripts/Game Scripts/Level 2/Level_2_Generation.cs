using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_2_Generation : MonoBehaviour
{                        // -------Green Side-----------// 
    public const float limit = 0.3f, Y = 0.2f, GreenSideX_Min = -1.85f, GreenSideX_Max = -0.85f, GreenSideZ_Min = -0.87f, GreenSideZ_Max = 0.26f,
                        //-------Purple Side-----------// 
                 PurpleSideX_Min = 0.32f, PurpleSideX_Max = 2.04f, PurpleSideZ_Min = GreenSideZ_Min, PurpleSideZ_Max = GreenSideZ_Max;
                        
                       //-------Orange Side-----------// 
    public const float OrangeSideX_Min = GreenSideX_Min, OrangeSideX_Max = GreenSideX_Max, OrangeSideZ_Min = 0.7f, OrangeSideZ_Max = 1.86f,
                       //-------Sky Side-----------// 
                     SkySideX_Min = PurpleSideX_Min, SkySideX_Max = PurpleSideX_Max , SkySideZ_Min = OrangeSideZ_Min , SkySideZ_Max = OrangeSideZ_Max;
                       

    public Vector3[] GreenSide_Cubes = new Vector3[10], PurpleSide_Cubes = new Vector3[10] , OrangeSide_Cubes = new Vector3[10] , SkySide_Cubes = new Vector3[10];
    public GameObject GreenCube_prefab, PurpleCube_prefab, OrangeCube_prefab, SkyCube_prefab;
    public static int GreenCube_Num , PurpleCube_Num , OrangeCube_Num , SkyCube_Num  , required_cubes ;


    public struct Area { public float Xmax; public float Xmin; public int CubesNum; public float Zmax; public float Zmin; public string name; };
    Area GreenArea, PurpleArea , OrangeArea, SkyArea;

    void Start()
    {
        GreenCube_Num = 0; PurpleCube_Num = 0; OrangeCube_Num = 0; SkyCube_Num = 0;

        GreenArea.Xmax = GreenSideX_Max; GreenArea.Xmin = GreenSideX_Min; GreenArea.Zmax = GreenSideZ_Max; GreenArea.Zmin = GreenSideZ_Min; GreenArea.name = "Green";
        PurpleArea.Xmax = PurpleSideX_Max; PurpleArea.Xmin = PurpleSideX_Min; PurpleArea.Zmax = PurpleSideZ_Max; PurpleArea.Zmin = PurpleSideZ_Min; PurpleArea.name = "Purple";
        OrangeArea.Xmax = OrangeSideX_Max; OrangeArea.Xmin = OrangeSideX_Min; OrangeArea.Zmax = OrangeSideZ_Max; OrangeArea.Zmin = OrangeSideZ_Min; OrangeArea.name = "Orange";
        SkyArea.Xmax = SkySideX_Max; SkyArea.Xmin = SkySideX_Min; SkyArea.Zmax = SkySideZ_Max; SkyArea.Zmin =SkySideZ_Min; SkyArea.name = "Sky";

        Cube_Spwan(GreenCube_prefab, required_cubes, PurpleArea, PurpleSide_Cubes);
        Cube_Spwan(OrangeCube_prefab, required_cubes, SkyArea, SkySide_Cubes);

        Cube_Spwan(PurpleCube_prefab, required_cubes, GreenArea, GreenSide_Cubes);
        Cube_Spwan(SkyCube_prefab, required_cubes, OrangeArea, OrangeSide_Cubes);
      //  Debug.Log(required_cubes);
        


    }


    private void Cube_Spwan(GameObject prefab, int number, Area area, Vector3[] Cubes)
    {
        
        int Cube_Num = 0;
        switch (area.name)
        {
            case "Green": Cube_Num = GreenCube_Num; break;
            case "Purple": Cube_Num = PurpleCube_Num; break;
            case "Orange": Cube_Num = OrangeCube_Num; break;
            case "Sky": Cube_Num = SkyCube_Num; break;
            
        }

        if (Cube_Num < 10)
        {

            for (int i = 0; i < number; i++)
            {
                Cubes[Cube_Num] = new Vector3(Random.Range(area.Xmin, area.Xmax), Y, Random.Range(area.Zmin, area.Zmax));
                Distance_Check(Cube_Num, area, Cubes);
                StartCoroutine(Generate_Cube(0.7f, prefab, Cubes, Cube_Num));
                Cube_Num++;
            }
            switch (area.name)
            {
                case "Green": GreenCube_Num = Cube_Num; break;
                case "Purple": PurpleCube_Num = Cube_Num; break;
                case "Orange": OrangeCube_Num = Cube_Num; break;
                case "Sky": SkyCube_Num = Cube_Num; break;

            }




        }


    }

    IEnumerator Generate_Cube(float waitTime, GameObject prefab, Vector3[] Cubes, int num)
    {
        GameObject SpwanedCube = Instantiate(prefab, Cubes[num], new Quaternion(0, 0, 0, 1));

        yield return new WaitForSeconds(waitTime);
    }
    private void Distance_Check(int cube_num, Area area, Vector3[] Cubes)
    {
        float distanceX, distanceZ;
        string flag;

        if (cube_num > 0)
        {
            for (int cnt = 1; cnt < (cube_num + 1); cnt++)
            {
                if (Cubes[cube_num].z >= Cubes[cube_num - cnt].z)
                {
                    distanceZ = Cubes[cube_num].z - Cubes[cube_num - cnt].z;

                }
                else
                {
                    distanceZ = Cubes[cube_num - cnt].z - Cubes[cube_num].z;

                }
                if (distanceZ < limit)
                {

                    if (Cubes[cube_num].x >= Cubes[cube_num - cnt].x)
                    {
                        distanceX = Cubes[cube_num].x - Cubes[cube_num - cnt].x;
                        flag = "greater_than";
                       // Debug.Log("greaterthanZ" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);
                    }
                    else
                    {
                        distanceX = Cubes[cube_num - cnt].x - Cubes[cube_num].x;
                        flag = "less_than";
                       // Debug.Log("lessthanZ" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);
                    }
                    if (distanceX < limit)
                    {
                        if (flag == "greater_than")
                        {
                            if (Cubes[cube_num].x + (limit - distanceX) < area.Xmax) Cubes[cube_num].x += (limit - distanceX);
                            else Cubes[cube_num].x -= (limit + distanceX);
                           // Debug.Log("greaterthanX" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);
                        }
                        else
                        {
                            if (Cubes[cube_num].x - (limit - distanceX) > area.Xmin) Cubes[cube_num].x -= (limit - distanceX);
                            else Cubes[cube_num].x += (limit + distanceX);
                          //  Debug.Log("lessthanX" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);

                        }
                    }
                }

            }
        }
    }
}
