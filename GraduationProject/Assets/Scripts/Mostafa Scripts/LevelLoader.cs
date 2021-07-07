using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator transition;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            StartCoroutine("LoadLevel");
        }
    }


    IEnumerator LoadLevel() {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(1);

    }
}
