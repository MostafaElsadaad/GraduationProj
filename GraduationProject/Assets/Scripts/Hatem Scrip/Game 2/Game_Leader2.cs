using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game_Leader2 : MonoBehaviour
{
    public static int points;
    private const int minute_second = 60, hour_second = 3600;
    private int pause_moment, Clock, pause_time, resume_time, Stop_Watch, Max_Score = 6;
    public TextMeshProUGUI score, Final_Score, Finish_Time;
    public GameObject Pause_Menu, InGame_Menu, Level_Completed;
    public static string diffcuilty, Level;
    public string PatientID, GameID, LevelID, MaxScore, Diffcuilty, TimeTaken, Score;
    // private bool Request_Sent ;
    public string Progress_URL = "http://8a20952c6e8c.ngrok.io/progress";
    void Start()
    {


        switch (diffcuilty)
        {
            case "easyToggle":
            case null:
                Diffcuilty = "easy";
                Hand_Move.speed = -1; 
                break;
            case "mediumToggle":
                Diffcuilty = "medium";
                Hand_Move.speed = -2;
                break;
            case "hardToggle":
                Diffcuilty = "hard";
                Hand_Move.speed = -3;
                break;
        }

        Moves_Generator.pauseButton_clicked = false;
        Hand_Move.cube_stop = false;
        points = 0; resume_time = 0; pause_moment = 0; Clock = 0; pause_time = 0; Stop_Watch = 0;
        Debug.Log(diffcuilty);

        InGame_Menu.gameObject.SetActive(true);
        Pause_Menu.gameObject.SetActive(false);
        Level_Completed.gameObject.SetActive(false);

        // ------------API--------------------//
        //Request_Sent = false;
        //  PatientID = API_Handler.Patient.id;
        // GameID = API_Handler.Game_Info.data[0].id;
        //  MaxScore = Max_Score.ToString();

    }



    void Update()
    {
        Score = points.ToString();
        score.text = points.ToString();
        Level_Complete();
        

    }
    public void OnPauseButton_Click()
    {
        InGame_Menu.gameObject.SetActive(false);
        Pause_Menu.gameObject.SetActive(true);
        pause_moment = Clock;
        Moves_Generator.pauseButton_clicked = true;
        Hand_Move.cube_stop = true;

    }
    public void OnResumeButton_Click()
    {

        InGame_Menu.gameObject.SetActive(true);
        Pause_Menu.gameObject.SetActive(false);
        pause_time = Clock - pause_moment;
        resume_time += pause_time;
        Moves_Generator.pauseButton_clicked = false;
        Hand_Move.cube_stop = false; 



    }
    public void RestartGame()
    {
        // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
      
        Resources.UnloadUnusedAssets();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void BackToMainMenu()
    {
        // Post_Stats();
        SceneManager.LoadSceneAsync("Start menu", LoadSceneMode.Single);


    }
    public void Level_Complete()
    {
        if (points == Max_Score)
        {
            InGame_Menu.gameObject.SetActive(false);
            Pause_Menu.gameObject.SetActive(false);
            Finish_Time.text = "TIME: " + Calculate_Time();
            Final_Score.text = "SCORE: " + Max_Score.ToString();
            Level_Completed.gameObject.SetActive(true);
            //  if (!Request_Sent)
            //  { Post_Stats(); Request_Sent = true; } 

        }
        else
        {
            Stop_Watch = (int)Time.timeSinceLevelLoad - resume_time;
            Clock = (int)Time.timeSinceLevelLoad;
        }

    }
    public string Calculate_Time()
    {
        int hour, minute, second;
        hour = (int)Stop_Watch / hour_second;
        minute = (int)Stop_Watch / minute_second;
        second = Stop_Watch;
        minute -= hour * 60;
        second -= minute * 60;
        TimeSpan t = new TimeSpan(hour, minute, second);
        return t.ToString();
    }
    public void Post_Stats()
    {
        TimeTaken = Calculate_Time();
        string[] key = { "patientId", "gameId ", "levelId", "timeSpent", "score", "MaxScore", "Diffcuilty" };
        string[] value = { PatientID, GameID, LevelID, TimeTaken, Score, MaxScore, Diffcuilty };
        //Post_Request(key,value); 
        GameObject.Find("Manager").GetComponent<API_Handler>().Post_Request(key, value, Progress_URL);
    }

}
