using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diffcuilty : MonoBehaviour
{
    private ToggleGroup diff;
    public IEnumerator<Toggle> toggle;
    private GameObject[] play_buttons;
  


    void Update()
    {
        diff = GameObject.FindObjectOfType<ToggleGroup>();

        play_buttons = GameObject.FindGameObjectsWithTag("play_button");

        if (diff != null)
        {

            if (diff.AnyTogglesOn())
            {
                foreach (GameObject button in play_buttons)
                {
                    button.GetComponent<Button>().enabled = true;
                }
                toggle = diff.ActiveToggles().GetEnumerator();
                toggle.MoveNext();
                Toggle tog = toggle.Current;
                // Debug.Log(tog.name);
                Game_Leader.diffcuilty = tog.name;

            }
            else
            {
                foreach (GameObject button in play_buttons)
                {
                    button.GetComponent<Button>().enabled = false;
                }
            }
        }

    }
    public void Selected_Level( Button button)
    {
        Game_Leader.Level = button.name; 
    }
}
