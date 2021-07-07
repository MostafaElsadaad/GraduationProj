using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class sounds_functions : MonoBehaviour
{
    string[] backgroundMusic = { "Music1", "Music2"};

    private void Start() {
       // FindObjectOfType<AudioManager>().Play(backgroundMusic[Random.Range(0, 1)]);
    }

    public void clickonward() {
        FindObjectOfType<AudioManager>().Play("Clickforward");
    }
    public void clickbackward() {
        FindObjectOfType<AudioManager>().Play("Clickbackward");
    }


}
