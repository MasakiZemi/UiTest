using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

//敵の攻撃パターンを作成するためのエディター

public class SoundEditor : MonoBehaviour
{
    public Slider slider;   //再生バー用
    public int beat = 4;    //1小節に何拍打つか

    public enum ATTACKTYPE { Wave, End }

    [Serializable]
    public class EnemyAttackTime
    {
        public ATTACKTYPE attackType;   //敵の攻撃の種類
        public float musicScore;        //攻撃を出す時間
    }
    public List<EnemyAttackTime> timeList = new List<EnemyAttackTime>();
   

    AudioSource source;     //サウンド再生環境
    AudioClip clip;         //サウンドデータ

    float a;
    

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        clip = source.clip;

        //再生バーの終了位置セット
        //slider.maxValue = clip.length;

        //テキストの列分だけ回す
        int count = 0;
        foreach (string str in File.ReadLines("aaa.txt"))
        {
            timeList.Add(new EnemyAttackTime());                //リスト作成
            string[] arr = str.Split(',');                      //（,）カンマで分ける
            timeList[count].musicScore = float.Parse(arr[0]);   //テキストに書かれている時間の格納

            //enumを格納するときに名称として格納されたためそれ用に割り振りなおしている
            switch (arr[1])
            {
                case "Wave":timeList[count].attackType = ATTACKTYPE.Wave;break;
                case "End": timeList[count].attackType = ATTACKTYPE.End; break;
                default:break;
            }
            count++;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        //初めて作成するときのみ
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Beatの計算
            MusicBeatTime(beat);
        }

        //セーブ用
        if (Input.GetKeyDown(KeyCode.S))
        {
            //リスト内のデータを文字列に格納する
            List<string> strList = new List<string>();
            for (int i = 0; i < timeList.Count; i++)
            {
                strList.Add(timeList[i].musicScore + "," + timeList[i].attackType);
            }

            //テキストに書き出し
            File.WriteAllLines("aaa.txt", strList);
        }
    }

    void MusicBeatTime(int beat)
    {
        //リスト初期化
        timeList.Clear();

        //一小節の時間の計算
        //60*拍子*小節数/テンポ
        float barTime = 60 * Music.MyInspectorList[0].UnitPerBeat * 1 / (float)Music.MyInspectorList[0].Tempo;

        //拍子にする
        barTime = barTime / beat;

        //拍子リストの作成
        int count = 0;
        while (true)
        {
            timeList.Add(new EnemyAttackTime());                    //リスト作成
            timeList[count].musicScore = barTime * count;           //1曲に何拍打つかの計算
            if (timeList[count].musicScore >= clip.length) break;   //1曲の時間分すべて出すことができたらループから抜ける
            //if (count == 10) break;
            count++;
        }
    }

    //デバッグ用
    void DebugLog()
    {
        float barTime = 60 * Music.MyInspectorList[0].UnitPerBeat * 1 / (float)Music.MyInspectorList[0].Tempo;
        barTime = barTime / 4;

        float b = source.time - a;

        Debug.Log("引いた数" + b);

        a = source.time;

        Debug.Log("計算" + barTime);
        Debug.Log("リアル" + source.time);
    }
}
