using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// インフォメーション用のテキストを操作
/// </summary>
public class TextControl : MonoBehaviour
{
    public TextMesh enemyInfo;
    public TextMesh plInfo;
    public TextMesh timeInfo;
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        enemyInfo.text = "Enemy: " + MouseStatus.GetEnemyAttackType;
        plInfo.text = "PL: " + MouseStatus.GetPlStepTiming;
        timeInfo.text = "" + StepData.GetStepData[StepData.GetTimeNearBeatTime(slider.value)].musicScore+" Time";
    }
}
