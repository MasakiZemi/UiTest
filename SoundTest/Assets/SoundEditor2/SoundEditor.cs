﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;
using System;

//敵の攻撃パターンを作成するためのエディター

public class SoundEditor : MonoBehaviour
{
    public Slider slider;   //再生バー用
    public Text text;
    public int beat = 4;    //1小節に何拍打つか
    string scoreName;
    string fileName;

    public enum ATTACKTYPE
    {
        Nothing,
        WaveWide,
        Throw
    }

    public enum PL_STEP_TIMING
    {
        Nothing,
        Step,
    }

    [Serializable]
    public class EnemyAttackTime
    {
        public bool[] lane = new bool[6];
        public ATTACKTYPE attackType;   //敵の攻撃の種類
        public float musicScore;        //攻撃を出す時間
    }
    public List<EnemyAttackTime> timeList = new List<EnemyAttackTime>();

    public class PlStepTiming
    {
        public PL_STEP_TIMING stepTiming;
    }
    public List<PlStepTiming> player = new List<PlStepTiming>();


    List<float> timeCheck = new List<float>();  //時間のチェック用に使う

    AudioSource source;     //サウンド再生環境
    AudioClip clip;         //サウンドデータ

    float a;
    float barTiming;
    bool onMusic;
    bool onMusicStart;
    int listCount;

    #region カスタムインスペクター　timeListリストの初期化をしている
    public bool onInspector;
    [CustomEditor(typeof(SoundEditor))]
    public class CharacterEditor : Editor           // Editorを継承するよ！
    {
        int countInspector;
        SoundEditor chara;

        private void OnEnable()
        {
            //chara = target as SoundEditor;  //この宣言をしないと勝手に初期化されてしまう
        }

        public override void OnInspectorGUI()
        {
            chara = target as SoundEditor;

            //シークバー、テキスト、数値のアタッチ用
            chara.slider = EditorGUILayout.ObjectField("シークバー", chara.slider, typeof(Slider), true, GUILayout.Width(300)) as Slider;
            chara.text = EditorGUILayout.ObjectField("テキスト", chara.text, typeof(Text), true, GUILayout.Width(300)) as Text;
            chara.beat = EditorGUILayout.IntField("1小節に何拍打つか", chara.beat, GUILayout.Width(300));

            //リスト番号の操作
            EditorGUILayout.LabelField("\n");
            EditorGUILayout.LabelField("配列の操作");
            countInspector = EditorGUILayout.IntSlider(countInspector, 0, chara.timeList.Count - (1 + chara.beat), GUILayout.Width(300));
            int fix = countInspector / chara.beat;   //1小節ごとにインスペクター上に表示

            //想定したビート分だけ表示する
            //リスト番号を切り替えることで、表示されているもが変わる
            if (File.Exists(chara.fileName))
            {
                for (int f = 0; f < chara.beat; f++)
                {
                    //横並びにチェックボックスを表示(bool)
                    EditorGUILayout.LabelField("\n");

                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.LabelField("レーン:", GUILayout.Width(80));
                    for (int i = 0; i < 6; i++)
                    {
                        chara.timeList[fix * chara.beat + f].lane[i] = EditorGUILayout.Toggle(chara.timeList[fix * chara.beat + f].lane[i], GUILayout.Width(13));
                    }
                    float time = chara.timeList[fix * chara.beat + f].musicScore;
                    EditorGUILayout.LabelField("", GUILayout.Width(20));
                    EditorGUILayout.LabelField("時間:  "+ time);

                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    //Enumの表示
                    EditorGUILayout.LabelField("攻撃の種類:", GUILayout.Width(80));
                    chara.timeList[fix * chara.beat + f].attackType =
                        (SoundEditor.ATTACKTYPE)EditorGUILayout.EnumPopup("", chara.timeList[fix * chara.beat + f].attackType, GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    //プレイヤー
                    EditorGUILayout.LabelField("PLステップ:", GUILayout.Width(80));
                    chara.player[fix * chara.beat + f].stepTiming =
                        (SoundEditor.PL_STEP_TIMING)EditorGUILayout.EnumPopup("", chara.player[fix * chara.beat + f].stepTiming, GUILayout.Width(100));
                    EditorGUILayout.EndHorizontal();
                }
            }
            //target = chara;
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        clip = source.clip;
        source.Stop();

        //参照するパス
        scoreName = "aaa";
        fileName = "Assets/SoundEditor2/Score/" + scoreName + ".txt";

        //初期化インスペクター操作を入れると値がすべて保持されるとの初期化の文を書く必要性がある
        onMusic = false;
        onMusicStart = false;
        listCount = 0;
        timeCheck.Clear();

        //再生バーの終了位置セット
        slider.maxValue = clip.length;

        //インスペクターの式を入れると値が確保されっぱなしになるので、初期化の文を書かないと大変なことになる
        timeList.Clear();
        player.Clear();

        //テキストの列分だけ回す
        if (File.Exists(fileName))
        {
            foreach (string str in File.ReadLines(fileName))
            {
                timeList.Add(new EnemyAttackTime());
                player.Add(new PlStepTiming());

                string[] arr = str.Split(',');                           //（,）カンマで分ける
                timeList[listCount].musicScore = float.Parse(arr[0]);    //テキストに書かれている時間の格納

                //チェック用
                timeCheck.Add(float.Parse(arr[0]));

                //攻撃の種類を登録
                timeList[listCount].attackType = (ATTACKTYPE)int.Parse(arr[1]);

                //攻撃が放たれるレーン
                if (arr.Length > 2)
                {
                    for (int i = 0; i < timeList[listCount].lane.Length; i++)
                    {
                        timeList[listCount].lane[i] = bool.Parse(arr[2 + i]);
                    }
                }

                //プレイヤーステップの登録
                player[listCount].stepTiming = (PL_STEP_TIMING)int.Parse(arr[8]);

                listCount++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        SoundControl();

        if (Music.IsPlaying && Music.IsJustChangedBar() && !onMusicStart)
        {
            barTiming = 0;//source.time;
            onMusicStart = true;
            Debug.Log(barTiming);
        }

        //初めて作成するときのみ
        if (Input.GetKeyDown(KeyCode.C))
        {
            //Beatの計算
            MusicBeatTime(beat);
        }

        Save();

        if (listCount != 0) OutputBeatTime();
    }

    //操作系
    void SoundControl()
    {
        //BGMの再生時間と再生バーをリンク
        if (onMusic) slider.value = source.time;

        //マウスクリックをするとBGMが止まる
        if (Input.GetMouseButtonDown(0))
        {
            source.Stop();
            onMusic = false;
        }

        //スペースキーで再生
        if (Input.GetKeyDown(KeyCode.Space))
        {
            source.time = slider.value; //再生バーの位置とBGM再生位置をリンク
            onMusic = true;
            source.Play();              //BGM再生
        }
    }

    //リストのセーブ
    void Save()
    {
        //セーブ用
        if (Input.GetKeyDown(KeyCode.S))
        {
            //リスト内のデータを文字列に格納する
            List<string> strList = new List<string>();
            for (int i = 0; i < timeList.Count; i++)
            {
                strList.Add(timeList[i].musicScore + "," + (int)timeList[i].attackType +
                    "," + timeList[i].lane[0] + "," + timeList[i].lane[1] + "," + timeList[i].lane[2] +
                    "," + timeList[i].lane[3] + "," + timeList[i].lane[4] + "," + timeList[i].lane[5] +
                    "," + (int)player[i].stepTiming);
            }

            Debug.Log(fileName);

            //テキストに書き出し
            File.WriteAllLines(fileName, strList);
        }
    }

    //時間、リスト番号の表示
    void OutputBeatTime()
    {
        //目的の値に最も近い値を返す
        var min = timeCheck.Min(c => Math.Abs(c - slider.value));
        int num = timeCheck.IndexOf(timeCheck.First(c => Math.Abs(c - slider.value) == min));
        text.text = "リスト時間:" + timeCheck.First(c => Math.Abs(c - slider.value) == min) +
                    "\n    配列数   :" + num + "\nリアル時間:" + slider.value;
    }

    //リスト作成
    void MusicBeatTime(int beat)
    {
        //リスト初期化
        timeList.Clear();
        player.Clear();

        //一小節の時間の計算
        //60*拍子*小節数/テンポ
        float barTime = 60 * Music.MyInspectorList[0].UnitPerBeat * 1 / (float)Music.MyInspectorList[0].Tempo;

        //拍子にする
        barTime = barTime / beat + barTiming;

        //拍子リストの作成
        int count = 0;
        while (true)
        {
            //チェック用
            timeCheck.Add(barTime * count);

            player.Add(new PlStepTiming());
            timeList.Add(new EnemyAttackTime());                    //リスト作成
            timeList[count].musicScore = barTime * count;           //1曲に何拍打つかの計算
            timeList[count].attackType = ATTACKTYPE.Nothing;
            player[count].stepTiming = PL_STEP_TIMING.Nothing;      //プレイヤー
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