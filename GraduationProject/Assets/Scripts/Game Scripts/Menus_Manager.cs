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
    public void pianoTiles()
    {
        SceneManager.LoadScene(6); 
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
