using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private Text NightCount;
    [SerializeField] private Text NightSeconds;
    [SerializeField] private Image PieClock;
    [SerializeField] private int secondsInNight;
    private int totalElapsedSeconds = 0;
    private int seconds;
    private int night = 1;

    // Start is called before the first frame update
    void Start()
    {
        seconds = secondsInNight;
        NightCount.text = "Night " + night;
        InvokeRepeating("count", 0f, 1f);
    }


    // TODO: move UI related code to different script
    private void count(){
        totalElapsedSeconds++;
        seconds--;
        if (seconds == 0){
            seconds = secondsInNight;
            night++;
            NightCount.text = "Night " + night;
            print("The night has passed.");
        }
        NightSeconds.text = "" + seconds;
        PieClock.fillAmount = (float)seconds/(float)secondsInNight;
    }
}
