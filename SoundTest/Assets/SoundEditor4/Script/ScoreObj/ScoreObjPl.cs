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
        GameObject obj = PlModeGroup.obj;
        int num = (int)transform.localPosition.y;
        for (int i = 0; i < obj.transform.childCount - 1; i++)
        {
            StepData.PL_STEP_TIMING pl = StepData.GetStepData[num].plStep;
            if (pl == obj.transform.GetChild(i).gameObject.GetComponent<PlModeObj>().plModeType)
            {
                plStepTiming = pl;
                GetComponent<Renderer>().material = obj.transform.GetChild(i).gameObject.GetComponent<Renderer>().material;
                break;
            }
        }
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
