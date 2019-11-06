using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class InstantNotes : MonoBehaviour
{
    public class EnemyAttackTime
    {
        public bool[] lane = new bool[6];
        public SoundEditor.ATTACKTYPE attackType;   //敵の攻撃の種類
        public float musicScore;        //攻撃を出す時間
    }
    public List<EnemyAttackTime> timeList = new List<EnemyAttackTime>();

    public GameObject[] objPos = new GameObject[6];
    public GameObject obj;
    public GameObject destroyPos;
    public Slider slider;

    int listCount;
    float timer;
    List<GameObject> notesList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach (string str in File.ReadLines("aaa.txt"))
        {
            timeList.Add(new EnemyAttackTime());

            string[] arr = str.Split(',');                           //（,）カンマで分ける
            timeList[listCount].musicScore = float.Parse(arr[0]);    //テキストに書かれている時間の格納

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

    // Update is called once per frame
    void Update()
    {
        timer = slider.value;//Time.deltaTime;

        if (timer > timeList[listCount].musicScore)
        {
            for (int i = 0; i < timeList[listCount].lane.Length; i++)
            {
                if (timeList[listCount].lane[i])
                {
                    notesList.Add(Instantiate(obj, objPos[i].transform));
                }
            }
            listCount++;
        }

        Destroy();
    }
    void Destroy()
    {
        for(int i = 0; i < notesList.Count; i++)
        {
            if (notesList[i].transform.position.z < destroyPos.transform.position.z)
            {
                Destroy(notesList[i]);
                notesList.RemoveAt(i);
                if (notesList.Count < i) break;
            }
        }
    }
}
