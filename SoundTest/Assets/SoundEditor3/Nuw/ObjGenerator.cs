using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjGenerator : MonoBehaviour
{
    public Slider slider;
    public GameObject laneObj0;
    public GameObject laneObj1;
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
            if (slider.value != 0)
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
        for (int i = 0; i < StepData.GetStepData.Count; i++)
        {
            if (i % 2 == 0)
            {
                GameObject instant = Instantiate(laneObj0, new Vector3(0, i, 0.001f), new Quaternion());
                instant.transform.parent = groupObj.transform;
            }
            else
            {
                GameObject instant = Instantiate(laneObj1, new Vector3(0, i, 0.001f), new Quaternion());
                instant.transform.parent = groupObj.transform;
            }

        }

    }
}
