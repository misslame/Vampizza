using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureController : MonoBehaviour
{

    [SerializeField] private int stages;
    [SerializeField] private int currentStage;

    private Animator StructureAnimator;

    // Start is called before the first frame update
    void Start()
    {
        StructureAnimator = GetComponent<Animator>();
        stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stop(){
        StructureAnimator.SetFloat("speed", 0);
    }

    public void step(){
        StructureAnimator.SetFloat("speed", 100);
    }
}
