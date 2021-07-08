using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus_Manager : MonoBehaviour
{
    //Pause menu
    //public void BackToLevelsMenu()
    //{
      //  SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    //}

    //pause menu + level complete menu
    public void RestartGame()
    {
       // SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
        Resources.UnloadUnusedAssets();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name , LoadSceneMode.Single);
    }


    //level complete menu
    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync("Start menu" , LoadSceneMode.Single);
       
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
        SceneManager.LoadSceneAsync("Level One", LoadSceneMode.Single);
    }

    public void levelTwo()
    {
        SceneManager.LoadSceneAsync("Level Two", LoadSceneMode.Single);
    }

    public void levelThree()
    {
        SceneManager.LoadSceneAsync("Level Three", LoadSceneMode.Single);
    }

    public void levelFour()
    {
        SceneManager.LoadSceneAsync("Level Four", LoadSceneMode.Single);
    }
    public void pianoTiles()
    {
        SceneManager.LoadSceneAsync("PianoTiles", LoadSceneMode.Single);
    }

    //main menu
    //public void CubesLevelsMenu()
    //{
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //}

    public void quitGame()
    {
       // Debug.Log("Quit!");
        Application.Quit();
    }
}
