using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureController : MonoBehaviour
{
    [Header("this field is a length. it is not 0-indexed. if no stages, enter a number <= 0")]
    [SerializeField] private int stages;
    [SerializeField] private int totalTimeForAllAnimations;
    private int currentStage = 0;

    private Animator StructureAnimator;

    // Start is called before the first frame update
    void Start()
    {
        StructureAnimator = GetComponent<Animator>();
        print("structure controller start function");
        print(StructureAnimator);
        stop();
        if (stages > 0){
            Clock.Instance.addStepActionToQueue(this, totalTimeForAllAnimations/3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stop(){
        StructureAnimator.SetFloat("speed", 0);
    }

    public void step(){
        if (currentStage < stages - 1){
            StructureAnimator.SetFloat("speed", 5);
            currentStage++;
            Clock.Instance.addStepActionToQueue(this, totalTimeForAllAnimations/3);
            print("<color=green>successful step</color>");
        } else {
            print("<color=red>tried to step structure on its last stage</color>");
        }
    }

    public void reset(){
        
    }
}
