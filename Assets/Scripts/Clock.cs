using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    [SerializeField] private int secondsInNight;
    private int totalElapsedSeconds = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("count", 0f, 1f);
    }


    // TODO: move UI related code to different script
    private void count(){
        totalElapsedSeconds++;
    }
}
