using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureController : MonoBehaviour
{
    [SerializeField] private int stages;
    [SerializeField] private int totalSecondsForAllAnimations;
    [SerializeField] private int currentStage = 1;

    private Animator StructureAnimator;

    // Start is called before the first frame update
    void Start()
    {
        StructureAnimator = GetComponent<Animator>();
        stop();
        print(gameObject.name);
        addSelfToQueue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stop(){
        StructureAnimator.SetFloat("speed", 0);
    }

    public void step(){
        // just a safeguard... should never happen in production release
        if (StructureAnimator == null){
            print("animator is null...");
            return;
        }

        if (currentStage < stages){
            StructureAnimator.SetFloat("speed", 1);
            currentStage++;
            if (currentStage < stages)
                this.addSelfToQueue();
            print(currentStage + "/" + stages);
            print("<color=green>successful step</color>");
        } else {
            // this should never happen
            print("<color=red>tried to step structure on its last stage</color>");
        }
    }

    public void reset(){
        
    }

    public int getCurrentStage(){
        return currentStage;
    }
    public int getTotalStages(){
        return stages;
    }

    public void addSelfToQueue(){
        Clock.addStepActionToQueue((totalSecondsForAllAnimations* 10)/stages , this);
    }
}
