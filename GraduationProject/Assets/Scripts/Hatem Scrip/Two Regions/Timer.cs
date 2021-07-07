using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI Game_Time;
    public int  Stop_Watch,  resume_time   ; 
    void Start()
    {
        resume_time = 0;
    }

  
    void Update() 
    {
      
        Stop_Watch = (int)Time.time - resume_time;
        Game_Time.text = Stop_Watch.ToString();

    }
}
