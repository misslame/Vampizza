using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Clock : MonoBehaviour
{

    // Singleton stuff
    // i swear this is necessary
    private static Clock instance = null;
    public static Clock Instance {
        get {return instance;}
    }

    [SerializeField] private int secondsInNight;
    private int totalElapsedSeconds = 0;
    delegate void QueueAction();
    private Dictionary<int,List<QueueAction>> Queue = new Dictionary<int,List<QueueAction>>();
    
    // Start is called before the first frame update
    void Start()
    {

        // singleton stuff 
        if(instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // eventually will need some code to load/save totalElapsedSeconds + the queue

        InvokeRepeating("count", 0f, 1f);
    }

    private void count(){
        totalElapsedSeconds++;
        checkQueue();
    }

    private void checkQueue(){
        // ContainsKey is ~O(1) so doing this every second is fine
        if (Queue.ContainsKey(totalElapsedSeconds)){
            print("wowie time to do something!!!111!!111");
            foreach(QueueAction action in Queue[totalElapsedSeconds]){
                action();
            }
            Queue.Remove(totalElapsedSeconds);
        }
    }

    public void addStepActionToQueue(StructureController controller, int totalTime){
        print("added something to queue");
        QueueAction step = delegate () {
            controller.step();
        };
        int targetTime = totalElapsedSeconds + totalTime;
        if (Queue.ContainsKey(targetTime)){
            Queue[targetTime].Add(step);
        } else {
            Queue.Add(targetTime, new List<QueueAction>(){step});
        }
    }
}
