using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Clock : MonoBehaviour
{

    private static long totalElapsedSeconds = 0;

    private delegate void QueueAction();

    // Start is called before the first frame update
    private static Dictionary<long, List<QueueAction>> Queue;
    void Start()
    {
        Queue = new Dictionary<long, List<QueueAction>>();
        print(Queue.Count);
        // eventually will need some code to load/save totalElapsedSeconds + the queue
        InvokeRepeating("count", 0f, 0.1f);
    }

    private void count(){
        totalElapsedSeconds++;
        long currentTime = totalElapsedSeconds;
        if (Queue.ContainsKey(currentTime)){
            print(currentTime);
            for (int n = 0; n < Queue[currentTime].Count; n++){
                Queue[currentTime][n]();
            }
            print("timer reached an event");
            print(currentTime);
            print(Queue.Count);
            print(Queue[currentTime].Count);
        }
        if (Queue.ContainsKey(currentTime)){
            Queue.Remove(currentTime);
        }
    }

    public static void addStepActionToQueue(int time, StructureController controller){
        QueueAction stepAction = () => {
            controller.step();
        };

        if (Queue.ContainsKey(totalElapsedSeconds + time)){
            // what if multiple structures created at the same time? handle that here...
        } else {
            Queue.Add(totalElapsedSeconds + time, new List<QueueAction>(){stepAction});
        }
    }
}
