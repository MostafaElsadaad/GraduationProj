using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_1_Generation : MonoBehaviour {
    public const float limit = 0.4f, BlueSideX_Min = -1.89f, BlueSideX_Max = -0.158f, RedSideX_Min = 0.85f, RedSideX_Max = 2.043f, Y = 0.2f, Z_Min = -0.892f, Z_Max = 1.837f; // playground_coordinates

    public Vector3[] RedSide_Cubes = new Vector3[10], BlueSide_Cubes = new Vector3[10];
    public GameObject RedCube_prefab, BlueCube_prefab;
    public static int BlueCube_Num = 0, RedCube_Num = 0 , required_cubes = 0 ;


    public struct Area { public float Xmax; public float Xmin; public string name; };
    Area RedArea, BlueArea;

    void Start() {

        BlueCube_Num = 0 ; RedCube_Num = 0; 
        RedArea.Xmax = RedSideX_Max; RedArea.Xmin = RedSideX_Min; RedArea.name = "Red";
        BlueArea.Xmax = BlueSideX_Max; BlueArea.Xmin = BlueSideX_Min; BlueArea.name = "Blue";

        Cube_Spwan(RedCube_prefab, required_cubes, BlueArea, BlueSide_Cubes);
        Cube_Spwan(BlueCube_prefab, required_cubes, RedArea, RedSide_Cubes);
        Cube_Spwan(RedCube_prefab, 2, RedArea, RedSide_Cubes);
        Cube_Spwan(BlueCube_prefab, 2, BlueArea , BlueSide_Cubes);
      

    }

    private void Cube_Spwan(GameObject prefab, int number, Area area, Vector3[] Cubes) {
       
        int Cube_Num = 0;
        switch (area.name)
        {
            case "Red": Cube_Num = RedCube_Num; break;
            case "Blue": Cube_Num = BlueCube_Num; break;        
        }
        if (Cube_Num < 10) {

            for (int i = 0; i < number; i++) {
                Cubes[Cube_Num] = new Vector3(Random.Range(area.Xmin, area.Xmax), Y, Random.Range(Z_Min, Z_Max));
                Distance_Check(Cube_Num, area, Cubes);
                StartCoroutine(Generate_Cube(0.7f, prefab, Cubes, Cube_Num));
                Cube_Num++;
            }
            switch (area.name)
            {
                case "Red": RedCube_Num = Cube_Num; break;
                case "Blue":BlueCube_Num = Cube_Num; break;
            }
        }
    }

    IEnumerator Generate_Cube(float waitTime, GameObject prefab, Vector3[] Cubes, int num) {
        GameObject SpwanedCube = Instantiate(prefab, Cubes[num], new Quaternion(0, 0, 0, 1));

        yield return new WaitForSeconds(waitTime);
    }
    private void Distance_Check(int cube_num, Area area, Vector3[] Cubes) {
        float distanceX, distanceZ;
        string flag;

        if (cube_num > 0) {
            for (int cnt = 1; cnt < (cube_num + 1); cnt++) {
                if (Cubes[cube_num].z >= Cubes[cube_num - cnt].z) {
                    distanceZ = Cubes[cube_num].z - Cubes[cube_num - cnt].z;

                }
                else {
                    distanceZ = Cubes[cube_num - cnt].z - Cubes[cube_num].z;

                }
                if (distanceZ < limit) {

                    if (Cubes[cube_num].x >= Cubes[cube_num - cnt].x) {
                        distanceX = Cubes[cube_num].x - Cubes[cube_num - cnt].x;
                        flag = "greater_than";
                        //Debug.Log("greaterthanZ" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);
                    }
                    else {
                        distanceX = Cubes[cube_num - cnt].x - Cubes[cube_num].x;
                        flag = "less_than";
                       // Debug.Log("lessthanZ" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);
                    }
                    if (distanceX < limit) {
                        if (flag == "greater_than") {
                            if (Cubes[cube_num].x + (limit - distanceX) < area.Xmax) Cubes[cube_num].x += (limit - distanceX);
                            else Cubes[cube_num].x -= (limit + distanceX);
                           // Debug.Log("greaterthanX" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);
                        }
                        else {
                            if (Cubes[cube_num].x - (limit - distanceX) > area.Xmin) Cubes[cube_num].x -= (limit - distanceX);
                            else Cubes[cube_num].x += (limit + distanceX);
                           // Debug.Log("lessthanX" + cube_num + " " + (cube_num - cnt) + " " + area.Xmax);

                        }
                    }
                }

            }
        }
    }


}


