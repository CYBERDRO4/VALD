using System;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static bool onPause = false;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Pause)) {
            onPause = !onPause;
            Time.timeScale = Convert.ToSingle(onPause);
        } 
    }
}
