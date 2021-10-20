using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CropController : MonoBehaviour
{
    [SerializeField] private int stages;
    [SerializeField] private int totalSecondsForAllAnimations;
    [SerializeField] private int yieldAmount;
    private int currentStage;

    private Animator StructureAnimator;

    // Start is called before the first frame update
    void Start()
    {   
        currentStage = 1;
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
            StructureAnimator.SetFloat("speed", 2);
            currentStage++;
            if (currentStage < stages){
                this.addSelfToQueue();
            }
            // Debug.Log(this.GetInstanceID());
        } else {
            // this should never happen
            print("<color=red>tried to step structure on its last stage</color>");
        }
    }

    public void harvest(){
        StructureAnimator.SetFloat("speed", 2);
        addSelfToQueue();
        currentStage = 1;

        // this is dumb, ugly, and terrible
        InventoryAndShopController.Instance.inventory.AddItem(((Item)new Wheat(yieldAmount)));
    }

    public void addSelfToQueue(){
        Clock.addCropToQueue((totalSecondsForAllAnimations* 10)/stages , this);
    }

    public bool isDoneGrowing(){
        return currentStage == stages;
    }
}