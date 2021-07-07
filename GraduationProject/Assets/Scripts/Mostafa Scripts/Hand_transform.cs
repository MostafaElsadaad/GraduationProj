using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_transform : MonoBehaviour
{
    public Vector3 Hand_coordinates;
    public float X_Recieved = 50;
    public float Y_Recieved = 50;
    public float Z_Recieved = 50;
    // Start is called before the first frame update 
    /*    x:   Min:  -1.85      Max: 2.0063
          y:   Min:   0.25      Max: 2.35
          z:   Min:  -0.7       Max: 1.7
    */
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Hand_coordinates = new Vector3(Remap_Trans(X_Recieved, 0, 100, -1.85f, 2.0063f), Remap_Trans(Y_Recieved, 0, 100, 0.25f, 2.35f), Remap_Trans(Z_Recieved, 0, 100, -0.7f, 1.7f));
        gameObject.transform.position = Hand_coordinates;
    }


    public float Remap_Trans(float value ,float in_start,float in_end, float Out_start, float out_end) {
        return (value - in_start) / (in_end - in_start) * (out_end - Out_start) + Out_start;
    }

}
