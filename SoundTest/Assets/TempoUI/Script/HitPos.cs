using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitPos : MonoBehaviour
{
    float notesLeftPos;
    float notesRightPos;

    GameObject obj;
    GameObject obj1;

    int footPosNum;
    int footPosMaxNum;
    bool onTriggerEnter;

    KeyCode NotesKeyName;

    // Start is called before the first frame update
    void Start()
    {
        //ボタン
        NotesKeyName = KeyCode.Space;

        //足判定、方向の数
        footPosMaxNum = 8;
    }

    // Update is called once per frame
    void Update()
    {

        if (BeatUi.isNotesPopUp)
        {

            //listObj = BeatUi.notesLefts;
            notesLeftPos = BeatUi.notesLefts[0].GetComponent<RectTransform>().localPosition.x;
            notesRightPos = BeatUi.notesRights[0].GetComponent<RectTransform>().localPosition.x;

            //ボタン判定
            footPosNum = FootPosNum();


            //左のノーツの処理
            if (notesLeftPos >= -150f && notesLeftPos < 100f)
            {
                obj = BeatUi.notesLefts[0];

                if (Input.GetKeyDown(NotesKeyName))
                {
                    if (notesLeftPos <= 50f && notesLeftPos >= -30f)
                    {
                        Debug.Log("Excellent!!");
                    }
                    if (notesLeftPos < -30f && notesLeftPos >= -60f)
                    {
                        Debug.Log("Good!!");
                    }
                    if (notesLeftPos < -60f && notesLeftPos >= -150f)
                    {
                        Debug.Log("Bad!!");
                    }



                    BeatUi.notesLefts.RemoveAt(0);
                    Destroy(obj);

                }
            }


            //消す
            if (notesLeftPos > 2f)
            {
                obj.GetComponent<Image>().color = Color.clear;
            }
            if (notesLeftPos > 100f)
            {
                BeatUi.notesLefts.RemoveAt(0);
                Destroy(obj);
            }


            //右のノーツの処理
            if (notesRightPos <= 150f && notesRightPos > -100f)
            {
                obj1 = BeatUi.notesRights[0];

                if (Input.GetKeyDown(NotesKeyName))
                {
                    BeatUi.notesRights.RemoveAt(0);
                    Destroy(obj1);
                }
            }
            if (!Input.GetKeyDown(NotesKeyName))
            {
                if (notesRightPos < -2f)
                {
                    obj1.GetComponent<Image>().color = Color.clear;
                }
                if (notesRightPos < -100f)
                {
                    BeatUi.notesRights.RemoveAt(0);
                    Destroy(obj1);
                }
            }
        }
    }

    void MelodyPattern()
    {
        List<int> musicList = new List<int>();


        if (Music.IsPlaying && Music.IsJustChangedBar())
        {
            for (int i = 0; i < musicList.Count; i++)
            {
                for (int f = 0; f < footPosMaxNum; f++)
                {
                    if (musicList[i] == f)
                    {
                        //攻撃種類の処理を書く
                    }
                }
            }

            //すべて消す
            musicList.Clear();
        }

        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            musicList.Add(footPosNum);
        }
    }

    //デバッグ用ボタン判定
    int FootPosNum()
    {
        int Num = 0;

        if (Input.GetKeyDown(KeyCode.Alpha1)) Num = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) Num = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) Num = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) Num = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5)) Num = 4;
        if (Input.GetKeyDown(KeyCode.Alpha6)) Num = 5;
        if (Input.GetKeyDown(KeyCode.Alpha7)) Num = 6;
        if (Input.GetKeyDown(KeyCode.Alpha8)) Num = 7;
        return Num;
    }
}
