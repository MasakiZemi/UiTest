using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSeat : MonoBehaviour
{
    public GameObject scrollView;
    public GameObject content;
    public SoundEditor4 s;

    float scrollViewCenterPos;

    float move;
    float speed;
    int stepDataCount;
    bool onStart;

    // Start is called before the first frame update
    void Start()
    {
        //中間を求める
        scrollViewCenterPos = scrollView.GetComponent<RectTransform>().sizeDelta.y / 2;

        //時間
        float time = StepData.GetStepData[10].musicScore - StepData.GetStepData[9].musicScore;
        //速度の計算
        speed = s.space / time;
    }

    // Update is called once per frame
    void Update()
    {
        //時間になたった開始
        if(StepData.GetSoundPlayTime > StepData.GetStepData[s.activeUi/2].musicScore)
        {
            onStart = true;
        }

        if (onStart)
        {
            //スクロール処理
            move += speed * Time.deltaTime;
            content.GetComponent<RectTransform>().localPosition = Vector3.up * move;
        }
    }
}
