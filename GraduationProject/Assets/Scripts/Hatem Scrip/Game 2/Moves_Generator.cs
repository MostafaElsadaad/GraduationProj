using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves_Generator : MonoBehaviour
{
   public GameObject[] Moves = new GameObject[4];
    private float timer, waitingTime = 4.0f; 
    void Start()
    {
        Generate_Move();
    }

  
  void Update()
    {
        timer += Time.deltaTime; 
        if( timer > waitingTime)
        {
            timer = 0.0f;
            Generate_Move();

        }

        
    }
    void Generate_Move()
    {
        GameObject Spwaned_Move = GameObject.Instantiate(Moves[Random.Range(0 , Moves.Length)], new Vector3(Random.Range(-5.6f, 5.6f), 5.6f, 0), new Quaternion(0, 0, 0, 1));
        //Debug.Log("hatem");
    }
   
        
    
 
}
