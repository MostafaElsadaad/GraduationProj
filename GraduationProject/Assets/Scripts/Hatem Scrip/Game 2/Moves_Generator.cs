using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves_Generator : MonoBehaviour
{
   public GameObject[] Moves = new GameObject[4];
    private float timer, waitingTime = 4.0f;
    private int random_num , temp=18;
    public static bool pauseButton_clicked = false; 
    string move;
    void Start()
    {
        Generate_Move();
    }

  
  void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitingTime)
          {
            if (!pauseButton_clicked)
            {
                Generate_Move();
                timer = 0.0f;
            }
          }
        

    }
    void Generate_Move()
    {   
       
        random_num = Random.Range(0, Moves.Length);
        while (random_num == temp)
        {
            random_num = Random.Range(0, Moves.Length);  
        }

        GameObject Spwaned_Move = GameObject.Instantiate(Moves[random_num], new Vector3(Random.Range(-5.6f, 5.6f), 5.6f, 0), new Quaternion(0, 0, 0, 1));
        temp = random_num;

    }
  
    public void Index_Opos()
    {
        move = "index_opos";
        Track_Move(move);
        Debug.Log(move);
    }
    public void Middle_Opos()
    {
        move = "middle_opos";
        Track_Move(move);
        Debug.Log(move);
    }
    public void Ring_Opos()
    {
        move = "ring_opos";
        Track_Move(move); 
        Debug.Log(move);

    }
    public void Pinky_Opos()
    {
        move = "pinky_opos"; 
        Track_Move(move);
        Debug.Log(move);

    }
    private void Track_Move(string move)
    {
        if (!string.IsNullOrEmpty(move))
        {
            if (GameObject.FindGameObjectsWithTag(move).Length > 0)
            {
                Destroy(GameObject.FindGameObjectsWithTag(move)[0]);
                Game_Leader2.points++;
            }
        }
    }




}
