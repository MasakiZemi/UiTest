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

    public float speed = 10;                        //ノーツ速度
    public GameObject[] objPos = new GameObject[6]; //ノーツ生成場所
    public GameObject obj;                          //生成元のオブジェクト
    public GameObject destroyPos;                   //壊すポジション
    //public GameObject linePos;
    public Slider slider;
    public Material throwMaterial;

    int listCount;                                          //リストの中身を回すよう
    float timer;                                            //経過時間
    List<GameObject> notesList = new List<GameObject>();    //ノーツオブジェクトの管理用
    float fixTime;                                          //修正する時間
    bool onMusicStart;                                      //再生フラグ
    List<float> timeCheck = new List<float>();              //リセット時に使う(リストに格納されている中んで一番近い時間を出す)

    // Start is called before the first frame update
    void Start()
    {
        //ラインに触れるタイミングが登録された時間になるようにするための計算
        fixTime = objPos[0].transform.position.z / speed;

        OnStart();
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

        Move();
    }
    void Move()
    {
        for(int i = 0; i < notesList.Count; i++)
        {
            if (notesList[i].transform.position.z < destroyPos.transform.position.z)
            {
                //削除
                Destroy(notesList[i]);
                notesList.RemoveAt(i);
                if (notesList.Count > i) break;
            }
            else
            {
                //動き
                Vector3 pos = notesList[i].transform.position;
                pos.z -= speed * Time.deltaTime;
                notesList[i].transform.position = pos;
            }
        }
    }

    //再生時間と一番近い時間までのリストを消す
    void ResetList()
    {
        var min = timeCheck.Min(c => Math.Abs(c - slider.value));
        int num = timeCheck.IndexOf(timeCheck.First(c => Math.Abs(c - slider.value) == min));
        for (int i = 0; i < num; i++) timeList.RemoveAt(0);
    }

    //テキスト読み込み
    void OnStart()
    {
        //初期化
        timeList.Clear();
        timeCheck.Clear();
        listCount = 0;
        timer = 0;
        onMusicStart = true;

        //テキストの読み込み
        foreach (string str in File.ReadLines("aaa.txt"))
        {
            timeList.Add(new EnemyAttackTime());

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

            listCount++;
        }
        listCount = 0;
    }
}
