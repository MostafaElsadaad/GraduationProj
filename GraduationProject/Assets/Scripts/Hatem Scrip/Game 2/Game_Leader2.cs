using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Leader2 : MonoBehaviour
{
    public static int points;
   
    private int pause_moment, Clock, pause_time, resume_time, Stop_Watch, Max_Score = 4;
    public TextMeshProUGUI score, Final_Score, Finish_Time;
    public GameObject Pause_Menu, InGame_Menu, Level_Completed;
    public static string diffcuilty, Level;

    void Start()
    {


        switch (diffcuilty)
        {
            case "easyToggle":
             
                
                break;
            case "mediumToggle":
               
                break;
            case "hardToggle":
              
                break;
        }
        switch (Level)
        {
            case "Level One":
            case "Level Three":
               
                break;

            case "Level Two":
     
             
                break;
            case "Level Four":
 
                break;


        }

        points = 0; resume_time = 0; pause_moment = 0; Clock = 0; pause_time = 0; Stop_Watch = 0;
       // Debug.Log(diffcuilty);
       // Debug.Log(Level);
        InGame_Menu.gameObject.SetActive(true);
        Pause_Menu.gameObject.SetActive(false);
        Level_Completed.gameObject.SetActive(false);


    }



    void Update()
    {

        Level_Complete();
        score.text = points.ToString();

    }
    public void OnPauseButton_Click()
    {
        InGame_Menu.gameObject.SetActive(false);
        Pause_Menu.gameObject.SetActive(true);
        pause_moment = Clock;
        Debug.Log("hjddf");


    }
    public void OnResumeButton_Click()
    {

        InGame_Menu.gameObject.SetActive(true);
        Pause_Menu.gameObject.SetActive(false);
        pause_time = Clock - pause_moment;
        resume_time += pause_time;



    }
    public void Level_Complete()
    {
        if (points == Max_Score)
        {
            InGame_Menu.gameObject.SetActive(false);
            Pause_Menu.gameObject.SetActive(false);
            Finish_Time.text = "TIME: " + Stop_Watch.ToString();
            Final_Score.text = "SCORE: " + Max_Score.ToString();
            Level_Completed.gameObject.SetActive(true);

        }
        else
        {
            Stop_Watch = (int)Time.timeSinceLevelLoad - resume_time;
            Clock = (int)Time.timeSinceLevelLoad;
        }

    }

}
