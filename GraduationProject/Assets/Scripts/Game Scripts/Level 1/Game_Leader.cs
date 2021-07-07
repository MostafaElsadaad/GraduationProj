using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Game_Leader : MonoBehaviour
{
    public static int points ;
    private Vector3 fence_height , fence_pos ;
    private int pause_moment , Clock , pause_time , resume_time, Stop_Watch, Max_Score , Level_Cubes;
    public TextMeshProUGUI score , Final_Score , Finish_Time;
    public GameObject Pause_Menu, InGame_Menu, Level_Completed ;
    public static string diffcuilty , Level  ; 
    
    void Start()
    {
       

        switch (diffcuilty)
        {
            case "easyToggle":
                Level_Cubes = 3;
                if (Level == "Level Four") { fence_height = new Vector3(0, 0.2f, 0); fence_pos = new Vector3(0, 0.1f, 0); }
                break;
            case "mediumToggle":
                Level_Cubes = 5;
                if (Level == "Level Four") { fence_height = new Vector3(0, 0.3f, 0); fence_pos = new Vector3(0, 0.15f, 0); }
                break;
            case "hardToggle":
                fence_height = new Vector3();
                if (Level == "Level Four") { fence_height = new Vector3(0, 0.4f, 0); fence_pos = new Vector3(0, 0.2f, 0); }
                Level_Cubes = 7;
                break;
        }
        switch (Level)
        {
            case "Level One":
            case "Level Three":
                Level_1_Generation.required_cubes = Level_Cubes;
                Max_Score = Level_Cubes * 2; 
                break;

            case "Level Two":
                Level_2_Generation.required_cubes = Level_Cubes;
                Max_Score = Level_Cubes * 4;
                this.gameObject.GetComponent<Level_2_Generation>().enabled = true; 
               // Debug.Log(Level_2_Generation.required_cubes);
                break;
            case "Level Four":
                GameObject.Find("barrier").GetComponent<Transform>().localScale += fence_height;
                GameObject.Find("barrier").GetComponent<Transform>().position += fence_pos;
                Level_1_Generation.required_cubes = Level_Cubes;
                Max_Score = Level_Cubes * 2;
                break;


        }
        
        points = 0; resume_time = 0; pause_moment = 0; Clock = 0; pause_time = 0; Stop_Watch = 0;
        //  Debug.LogError("hatem");
        Debug.Log(diffcuilty);
        Debug.Log(Level);
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
        pause_moment =  Clock ;
        
        
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
        if(points == Max_Score)
        {
            InGame_Menu.gameObject.SetActive(false);
            Pause_Menu.gameObject.SetActive(false);
            Finish_Time.text = "TIME: " + Stop_Watch.ToString();
            Final_Score.text = "SCORE: " + Max_Score.ToString();
            Level_Completed.gameObject.SetActive(true);

        }
        else { 
            Stop_Watch = (int)Time.timeSinceLevelLoad - resume_time;
            Clock = (int)Time.timeSinceLevelLoad;
        }

    }

}
