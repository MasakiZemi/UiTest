using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using System;

public class InstantNotes : MonoBehaviour
{
    public class EnemyAttackTime
    {
        public bool[] lane = new bool[6];           //
        public SoundEditor.ATTACKTYPE attackType;   //敵の攻撃の種類
        public float musicScore;                    //攻撃を出す時間
    }
    public List<EnemyAttackTime> timeList = new List<EnemyAttackTime>();

    public class PlStepTiming
    {
        public SoundEditor.PL_STEP_TIMING stepTiming;
    }
    public List<PlStepTiming> player = new List<PlStepTiming>();

    public string scoreName = "aaa";
    string fileName;

    public float speed = 10;                        //ノーツ速度
    public GameObject[] objPos = new GameObject[6]; //ノーツ生成場所
    public GameObject obj;                          //生成元のオブジェクト
    public GameObject plObj;                        //プレイヤーのステップ
    public GameObject destroyPos;                   //壊すポジション
    //public GameObject linePos;
    public Slider slider;
    public Material throwMaterial;

    int listCount;                                          //リストの中身を回すよう
    float timer;                                            //経過時間
    List<GameObject> notesList = new List<GameObject>();    //ノーツオブジェクトの管理用
    List<GameObject> plNotesList = new List<GameObject>();  //プレイヤーのノーツオブジェクト管理用
    float fixTime;                                          //修正する時間
    bool onMusicStart;                                      //再生フラグ
    List<float> timeCheck = new List<float>();              //リセット時に使う(リストに格納されている中んで一番近い時間を出す)

    // Start is called before the first frame update
    void Start()
    {
        //ラインに触れるタイミングが登録された時間になるようにするための計算
        fixTime = objPos[0].transform.position.z / speed;

        fileName = "Assets/SoundEditor2/Score/" + scoreName + ".txt";

        OnStart();

        onMusicStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (onMusicStart)
        {
            //シークバーの位置と連動させる
            timer = slider.value;//Time.deltaTime;

            //生成
            if (timer > timeList[listCount].musicScore - fixTime)
            {
                for (int i = 0; i < timeList[listCount].lane.Length; i++)
                {
                    if (timeList[listCount].lane[i])
                    {
                        notesList.Add(Instantiate(obj, objPos[i].transform));

                        //スローの時は色を変える
                        if (timeList[listCount].attackType == SoundEditor.ATTACKTYPE.Throw)
                        {
                            notesList[notesList.Count - 1].GetComponent<Renderer>().material = throwMaterial;
                        }
                    }
                }

                //プレイヤーのノーツ
                if (player[listCount].stepTiming != SoundEditor.PL_STEP_TIMING.Nothing)
                {
                    plNotesList.Add(Instantiate(plObj, Vector3.forward * objPos[0].transform.position.z, new Quaternion()));
                }
                Debug.Log("ステップ" + player[listCount].stepTiming + "カウント" + listCount);
                listCount++;
            }
        }

        //初期化
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnStart();
            ResetList();
        }
        if (Input.GetMouseButtonDown(0))
        {
            onMusicStart = false;
        }

        notesList = new List<GameObject>(Move(notesList));
        plNotesList = new List<GameObject>(Move(plNotesList));
    }

    //動き
    List<GameObject> Move(List<GameObject> objList)
    {
        for (int i = 0; i < objList.Count; i++)
        {
            if (objList[i].transform.position.z < destroyPos.transform.position.z)
            {
                //削除
                Destroy(objList[i]);
                objList.RemoveAt(i);
                if (objList.Count > i) break;
            }
            else
            {
                //動き
                Vector3 pos = objList[i].transform.position;
                pos.z -= speed * Time.deltaTime;
                objList[i].transform.position = pos;
            }
        }

        return objList;
    }

    //再生時間と一番近い時間までのリストを消す
    void ResetList()
    {
        var min = timeCheck.Min(c => Math.Abs(c - slider.value));
        int num = timeCheck.IndexOf(timeCheck.First(c => Math.Abs(c - slider.value) == min));
        for (int i = 0; i < num; i++)
        {
            timeList.RemoveAt(0);
            player.RemoveAt(0);
            listCount++;
        }
    }

    //テキスト読み込み
    void OnStart()
    {
        //初期化
        timeList.Clear();
        player.Clear();
        timeCheck.Clear();
        listCount = 0;
        timer = 0;

        onMusicStart = true;

        //テキストの読み込み
        foreach (string str in File.ReadLines(fileName))
        {
            timeList.Add(new EnemyAttackTime());
            player.Add(new PlStepTiming());

            string[] arr = str.Split(',');                           //（,）カンマで分ける
            timeList[listCount].musicScore = float.Parse(arr[0]);    //テキストに書かれている時間の格納
            timeCheck.Add(float.Parse(arr[0]));                      //リセット用

            //攻撃の種類を登録
            timeList[listCount].attackType = (SoundEditor.ATTACKTYPE)int.Parse(arr[1]);

            //攻撃が放たれるレーン
            if (arr.Length > 2)
            {
                for (int i = 0; i < timeList[listCount].lane.Length; i++)
                {
                    timeList[listCount].lane[i] = bool.Parse(arr[2 + i]);
                }
            }

            //プレイヤーステップの登録
            player[listCount].stepTiming = (SoundEditor.PL_STEP_TIMING)int.Parse(arr[8]);

            listCount++;
        }
        listCount = 0;
    }
}
