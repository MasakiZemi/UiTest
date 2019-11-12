using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StepData : MonoBehaviour
{
    public string scoreName = "aaa";
    string fileName = "Assets/SoundEditor2/Score/";

    public AudioSource source;     //サウンド

    public enum INPUT_TEXT { MusicScore, PlStep = 8 }
    public enum PL_STEP_TIMING
    {
        Nothing,
        Step,
    }

    public class Data
    {
        public PL_STEP_TIMING plStep;
        public float musicScore;
    }
    public List<Data> stepData = new List<Data>();

    static StepData StepData_ = new StepData();

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        stepData.Clear();
        fileName += scoreName + ".txt";

        //Debug.Log(File.Exists(fileName));

        //テキストの読み込み
        foreach (string str in File.ReadLines(fileName))
        {
            string[] arr = str.Split(',');                           //（,）カンマで分ける
            stepData.Add(new Data());

            stepData[count].musicScore = float.Parse(arr[(int)INPUT_TEXT.MusicScore]);
            stepData[count].plStep = (PL_STEP_TIMING)int.Parse(arr[(int)INPUT_TEXT.PlStep]);

            count++;
        }

        //StepData_.stepData = GetComponent<StepData>();
        StepData_.stepData = new List<Data>(stepData);
        StepData_.source = source;
    }

    // Update is called once per frame
    void Update()
    {
        //StepData_.source = source;
    }

    public static List<Data> GetStepData { get { return StepData_.stepData; } }
    public static float GetSoundPlayTime { get { return StepData_.source.time; } }
}
