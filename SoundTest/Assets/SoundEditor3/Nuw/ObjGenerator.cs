using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjGenerator : MonoBehaviour
{
    public Slider slider;
    public GameObject laneObj;
    public GameObject groupObj;
    public Material laneMaterial0, laneMaterial1;

    float speed;
    float move;
    bool onMusicPlay;

    // Start is called before the first frame update
    void Start()
    {
        Instant();
        speed = StepData.GetStepData.Count / StepData.GetSoundMaxTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (SoundControl.onMusic)
        {
            if (slider.value !=0)
            {
                move -= speed * Time.deltaTime;
                groupObj.transform.position = new Vector3(0, move, 0);
            }
            else
            {
                groupObj.transform.position = Vector3.zero;
            }
        }
        else
        {
            int num = -StepData.GetTimeNearBeatTime(slider.value);
            groupObj.transform.position = new Vector3(0, num, 0);
            move = num;
        }
    }

    void Instant()
    {
        int num = 0;
        for (int i = 0; i < StepData.GetStepData.Count; i++)
        {
            for (int f = 0; f < StepData.GetStepData[0].enemyAttackPos.Length; f++)
            {
                GameObject instant = Instantiate(laneObj, new Vector3(f, i, 0.001f), new Quaternion());
                instant.transform.parent = groupObj.transform;
                if (f % 2 == num)
                {
                    instant.GetComponent<Renderer>().material = laneMaterial0;
                }
                else
                {
                    instant.GetComponent<Renderer>().material = laneMaterial1;
                }
            }
            if (num == 0) num = 1;
            else num = 0;
        }
    }
}
