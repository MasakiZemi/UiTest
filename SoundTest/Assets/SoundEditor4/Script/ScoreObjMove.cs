using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreObjMove : MonoBehaviour
{
    public Slider slider;
    float speed;
    float move;
    // Start is called before the first frame update
    void Start()
    {
        speed = StepData.GetStepData.Count / StepData.GetSoundMaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (SoundControl.onMusic)
        {
            if (slider.value != 0)
            {
                //グループの移動
                move -= speed * Time.deltaTime;
                transform.position = new Vector3(0, move, 0);
            }
            else
            {
                //初期座標へ
                transform.position = Vector3.zero;
            }
        }
        else
        {
            //ポジションを自然数に変換する
            int num = -StepData.GetTimeNearBeatTime(slider.value);
            transform.position = new Vector3(0, num, 0);
            move = num;
        }
    }
}
