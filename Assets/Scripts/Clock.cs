using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField] private uint secondsInNight;
    private uint totalElapsedSeconds = 0;
    private uint seconds = 0;
    private uint night = 1;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("count", 1f, 1f);
    }

    private void count(){
        totalElapsedSeconds++;
        seconds++;
        if (seconds > secondsInNight){
            seconds = 0;
            night++;
            print("The night has passed.");
        }
    }
}
