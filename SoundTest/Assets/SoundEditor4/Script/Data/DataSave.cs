using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// テキストにデータを上書きすることができる
/// </summary>
public class DataSave : MonoBehaviour
{
    public int bar = 4;         //拍子
    public int beat = 16;       //1小節の刻む数
    public int tempo = 120;     //BMP

    float barTime;

    //胆略か用
    GameObject boolObjGroup;
    GameObject plObjGroup;
    GameObject enemyObjGroup;

    public List<StepData.Data> dataList = new List<StepData.Data>();

    // Start is called before the first frame update
    void Start()
    {
        barTime = 60 * (float)bar * 1 / (float)tempo;
        barTime = barTime / beat;
        int count = 0;

        while (true)
        {
            dataList.Add(new StepData.Data());

            //テンポ時間の計算
            float time = barTime * count;
            if (time >= StepData.GetSoundMaxTime) break;
            count++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            //Create();
        }
    }

    void Save()
    {
        
        List<string> strList = new List<string>();
        int boolCount = 0, count = 0;

        //一小節の時間の計算
        //60*拍子*小節数/テンポ
        barTime = 60 * (float)bar * 1 / (float)tempo;
        barTime = barTime / beat;

        //子オブジェクトを取得
        boolObjGroup = transform.GetChild(0).gameObject;
        plObjGroup = transform.GetChild(1).gameObject;
        enemyObjGroup = transform.GetChild(2).gameObject;

        while (true)
        {
            //敵の攻撃座標
            for (int i = (int)StepData.INPUT_TEXT.EnemyAttackLane0 - 2; i <= (int)StepData.INPUT_TEXT.EnemyAttackLane5 - 2; i++)
            {
                //オブジェクトの位置をもとにデータを代入していく
                int x = (int)boolObjGroup.transform.GetChild(boolCount).gameObject.transform.localPosition.x;
                int y = (int)boolObjGroup.transform.GetChild(boolCount).gameObject.transform.localPosition.y;
                //オブジェクトが持っている情報を取得する
                ScoreObjBool objBoolScript = boolObjGroup.transform.GetChild(boolCount).gameObject.GetComponent<ScoreObjBool>();
                dataList[y].enemyAttackPos[x] = objBoolScript.on;
                boolCount++;
            }

            //テンポ時間の計算
            float time = barTime * count;

            //プレイヤーのノーツ
            ScoreObjPl objPlScript = plObjGroup.transform.GetChild(count).gameObject.GetComponent<ScoreObjPl>();
            dataList[count].plStep = objPlScript.plStepTiming;

            //敵の攻撃種類
            ScoreObjEnemy objEnemyScript = enemyObjGroup.transform.GetChild(count).gameObject.GetComponent<ScoreObjEnemy>();
            dataList[count].ememyAttackType = objEnemyScript.enemyAttackType;

            //リストに格納
            strList.Add(time + "," + (int)dataList[count].ememyAttackType +
                    "," + dataList[count].enemyAttackPos[0] + "," + dataList[count].enemyAttackPos[1] +
                    "," + dataList[count].enemyAttackPos[2] + "," + dataList[count].enemyAttackPos[3] +
                    "," + dataList[count].enemyAttackPos[4] + "," + dataList[count].enemyAttackPos[5] +
                    "," + (int)dataList[count].plStep);

            if (time >= StepData.GetSoundMaxTime) break;
            count++;
        }

        //テキストに書き出し
        File.WriteAllLines(StepData.GetScoreLink, strList);

    }

    void Create()
    {
        List<string> strList = new List<string>();
        int boolCount = 0, count = 0;

        //一小節の時間の計算
        //60*拍子*小節数/テンポ
        barTime = 60 * (float)bar * 1 / (float)tempo;
        barTime = barTime / beat;

        while (true)
        {
            //敵の攻撃座標
            for (int i = (int)StepData.INPUT_TEXT.EnemyAttackLane0 - 2; i <= (int)StepData.INPUT_TEXT.EnemyAttackLane5 - 2; i++)
            {
                dataList[count].enemyAttackPos[i] = false;
                //boolCount++;
            }

            //テンポ時間の計算
            float time = barTime * count;

            //プレイヤーのノーツ
            dataList[count].plStep = StepData.PL_STEP_TIMING.Nothing;

            //敵の攻撃種類
            dataList[count].ememyAttackType = StepData.ENEMY_ATTACK_TYPE.Nothing;

            //リストに格納
            strList.Add(time + "," + (int)dataList[count].ememyAttackType +
                    "," + dataList[count].enemyAttackPos[0] + "," + dataList[count].enemyAttackPos[1] +
                    "," + dataList[count].enemyAttackPos[2] + "," + dataList[count].enemyAttackPos[3] +
                    "," + dataList[count].enemyAttackPos[4] + "," + dataList[count].enemyAttackPos[5] +
                    "," + (int)dataList[count].plStep);

            if (time >= StepData.GetSoundMaxTime) break;
            count++;
        }

        //テキストに書き出し
        File.WriteAllLines(StepData.GetScoreLink, strList);
    }
}
