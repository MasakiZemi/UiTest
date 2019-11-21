using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StepData : MonoBehaviour
{
    public string scoreName = "aaa";
    public string fileName = "Assets/SoundEditor2/Score/";

    public AudioSource source;      //サウンド

    public enum INPUT_TEXT           //テキストデータの種類
    {
        MusicScore,

        EnemyAttackType,
        EnemyAttackLane0, EnemyAttackLane1, EnemyAttackLane2,
        EnemyAttackLane3, EnemyAttackLane4, EnemyAttackLane5,

        PlStep
    }

    public enum PL_STEP_TIMING      //プレイヤーのノーツの種類
    {
        Nothing,
        Step,
    }
    public enum ENEMY_ATTACK_TYPE   //敵の攻撃の種類
    {
        Nothing,
        WaveWide,
        Throw
    }

    public class Data
    {
        public PL_STEP_TIMING plStep;               //プレイヤーノーツ

        public ENEMY_ATTACK_TYPE ememyAttackType;   //敵の攻撃タイプ
        public bool[] enemyAttackPos = new bool[6]; //敵の攻撃位置

        public float musicScore;                    //時間
    }
    public List<Data> stepData = new List<Data>();

    static StepData StepData_;  //自身を参照用

    // Start is called before the first frame update
    private void Awake()
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
            stepData[count].ememyAttackType = (ENEMY_ATTACK_TYPE)int.Parse(arr[(int)INPUT_TEXT.EnemyAttackType]);
            stepData[count].plStep = (PL_STEP_TIMING)int.Parse(arr[(int)INPUT_TEXT.PlStep]);

            for(int i=(int)INPUT_TEXT.EnemyAttackLane0;i<= (int)INPUT_TEXT.EnemyAttackLane5; i++)
            {
                stepData[count].enemyAttackPos[i - (int)INPUT_TEXT.EnemyAttackLane0] = bool.Parse(arr[i]);
            }

            count++;
        }

        StepData_ = this;   //初期化と数値の代入(thisしないとバグる)
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    public static List<Data> GetStepData { get { return StepData_.stepData; } }     //音楽データ渡す
    public static float GetSoundPlayTime { get { return StepData_.source.time; } }  //曲の再生時間渡す
}
