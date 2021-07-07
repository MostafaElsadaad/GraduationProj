using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Diffcuilty : MonoBehaviour
{
    private ToggleGroup diff;
    public IEnumerator<Toggle> toggle;
    
    void Start()
    {
       

    }

    void Update()
    {
        diff = GameObject.FindObjectOfType<ToggleGroup>();
        if (diff != null)
        {
            if (diff.AnyTogglesOn())
            {
                toggle = diff.ActiveToggles().GetEnumerator();
                toggle.MoveNext();
                Toggle tog = toggle.Current;
                // Debug.Log(tog.name);
                Game_Leader.diffcuilty = tog.name;
            }
        }
       
    }
    public void Selected_Level( Button button)
    {
        Game_Leader.Level = button.name; 
    }
}
