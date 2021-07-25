using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
public class Game_Leader : MonoBehaviour
{
    private const int minute_second = 60, hour_second = 3600; 
    public static int points ;
    private Vector3 fence_height , fence_pos ;
    public int Stop_Watch ;
    private int pause_moment , Clock , pause_time , resume_time, Max_Score , Level_Cubes;
    public TextMeshProUGUI score , Final_Score , Finish_Time;
    public GameObject Pause_Menu, InGame_Menu, Level_Completed ;
    public static string diffcuilty , Level  ;
    private string PatientID,  GameID , LevelID, MaxScore , Diffcuilty , TimeTaken , Score;
    private bool Request_Sent ;
    private string Progress_URL = "http://192.168.1.46:3000/progress";


    void Start()
    {  
 
        switch (diffcuilty)
        {   

            case "easyToggle":
            case null:
                Diffcuilty = "easy";
                Level_Cubes = 3;
                if (Level == "Level Four") { fence_height = new Vector3(0, 0.36f, 0); fence_pos = new Vector3(0, 0.18f, 0); }
                break;
            case "mediumToggle":
                Diffcuilty = "medium";
                Level_Cubes = 5;
                if (Level == "Level Four") { fence_height = new Vector3(0, 0.46f , 0); fence_pos = new Vector3(0, 0.23f, 0); }
                break;
            case "hardToggle":
                Diffcuilty = "hard";
                fence_height = new Vector3();
                if (Level == "Level Four") { fence_height = new Vector3(0, 0.56f, 0); fence_pos = new Vector3(0, 0.28f, 0); }
                Level_Cubes = 7;
                break;
            
        }
        switch (Level)
        {
            case "Level One":
            case null:
                LevelID = API_Handler.Game_Info.data[0].levels[0].id; 
                Level_1_Generation.required_cubes = Level_Cubes;
                Max_Score = Level_Cubes * 2;
                break;
            case "Level Two":
                LevelID = API_Handler.Game_Info.data[0].levels[1].id;
                Level_2_Generation.required_cubes = Level_Cubes;
                Max_Score = Level_Cubes * 4;
                this.gameObject.GetComponent<Level_2_Generation>().enabled = true;
                // Debug.Log(Level_2_Generation.required_cubes);
                break;
            case "Level Three":
                LevelID = API_Handler.Game_Info.data[0].levels[2].id;
                Level_1_Generation.required_cubes = Level_Cubes;
                Max_Score = (Level_Cubes * 2) + 3; 
                break;

            
            case "Level Four":
                LevelID = API_Handler.Game_Info.data[0].levels[3].id;
                GameObject.Find("barrier").GetComponent<Transform>().localScale += fence_height;
                GameObject.Find("barrier").GetComponent<Transform>().position += fence_pos;
                Level_1_Generation.required_cubes = Level_Cubes;
                Max_Score = Level_Cubes * 2;
                break;
        }

        
        points = 0; resume_time = 0; pause_moment = 0; Clock = 0; pause_time = 0; Stop_Watch = 0; 
        
        Debug.Log(diffcuilty);
        Debug.Log(Level);
        InGame_Menu.gameObject.SetActive(true);
        Pause_Menu.gameObject.SetActive(false);
        Level_Completed.gameObject.SetActive(false);

        // ------------API--------------------//
        Request_Sent = false;
         PatientID = API_Handler.Patient.id;
         GameID = API_Handler.Game_Info.data[0].id;
         MaxScore = Max_Score.ToString();

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
        pause_moment =  Clock ;
        
        
    }
    public void OnResumeButton_Click()
    {
        InGame_Menu.gameObject.SetActive(true);
        Pause_Menu.gameObject.SetActive(false);
        pause_time = Clock - pause_moment;
        resume_time += pause_time;
     
    }
    public void RestartGame()
    {
        // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void BackToMainMenu()
    {
        Post_Stats();
       SceneManager.LoadSceneAsync("Start menu", LoadSceneMode.Single);
        

    }

    public void NextLevel()
    {
        int current_scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene + 1);
        Scene next_scene = SceneManager.GetSceneByBuildIndex(current_scene + 1);
        Level = next_scene.name;
    }
    public void Level_Complete()
    {
        if(points == Max_Score)
        {
            InGame_Menu.gameObject.SetActive(false);
            Pause_Menu.gameObject.SetActive(false);
            Finish_Time.text = "TIME: " + Calculate_Time() ;
            Final_Score.text = "SCORE: " + Max_Score.ToString();
            Level_Completed.gameObject.SetActive(true);
            if (!Request_Sent)
            { Post_Stats(); Request_Sent = true; } 

        }
        else { 
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
        string[] key = { "patientId", "gameId " , "levelId", "timeSpent", "score", "maxScore" , "difficulty"};
        string[] value = { PatientID, GameID, LevelID, TimeTaken, Score, MaxScore, Diffcuilty };
        //Post_Request(key,value); 
        GameObject.Find("Manager").GetComponent<API_Handler>().Post_Request(key, value,Progress_URL);
    }

}
