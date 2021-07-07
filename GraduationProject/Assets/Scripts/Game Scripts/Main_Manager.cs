using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Manager : MonoBehaviour
{
    private ToggleGroup diff;
    public IEnumerator<Toggle> toggle;

    void Start()
    {
        
    }

    
    void Update()
    {
        diff = GameObject.FindObjectOfType<ToggleGroup>();
        if (diff.AnyTogglesOn())
        {
            toggle = diff.ActiveToggles().GetEnumerator();
            toggle.MoveNext();
            Toggle tog = toggle.Current;
            // Debug.Log(tog.name);
            Game_Leader.diffcuilty = tog.name;
        }
    }
    public void Selected_Level(Button button)
    {
        Game_Leader.Level = button.name;
    }
    public void RestartGame()
    {
        
        Resources.UnloadUnusedAssets();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }


    //level complete menu
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("Start menu", LoadSceneMode.Single);

    }

    public void NextLevel()
    {
        int current_scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(current_scene + 1);
        Scene next_scene = SceneManager.GetSceneByBuildIndex(current_scene + 1);
        Game_Leader.Level = next_scene.name;




    }

    //cubes levels menu
    public void levelOne()
    {
         
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void levelTwo()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    public void levelThree()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }

    public void levelFour()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 4);
    }

    //main menu
    //public void CubesLevelsMenu()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    public void quitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
}
