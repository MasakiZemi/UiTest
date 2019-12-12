using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObjPl : MonoBehaviour
{
    public bool onClick;
    public StepData.PL_STEP_TIMING plStepTiming;

    // Start is called before the first frame update
    void Start()
    {
        plStepTiming = StepData.PL_STEP_TIMING.Nothing;
    }

    // Update is called once per frame
    void Update()
    {
        if (onClick)
        {
            GetComponent<Renderer>().material = MouseStatus.GetPlMaterial;
            plStepTiming = MouseStatus.GetPlStepTiming;
            onClick = false;
        }
    }
}
