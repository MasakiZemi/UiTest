using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using System;

public class PlGroundNotesControl : MonoBehaviour
{
    public float speed = 5;
    public float lostPoint = 5;

    List<float> moveList = new List<float>();
    List<float> blockPosZ = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        //変換用
        for(int i = 0; i < GroundBloc.objList.Count; i++)
        {
            blockPosZ.Add(GroundBloc.objList[i].transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveList.Add(0f);
        }

        for (int f = 0; f < moveList.Count; f++)
        {
            //動く
            moveList[f] += speed * Time.deltaTime;

            //一個前のブロックを非表示にする
            if (blockPosZ.Count - 1 != PosNearListPos(moveList[f]))
            {
                GroundBloc.objList[PosNearListPos(moveList[f])].SetActive(true);
            }
            //数値に一番近いブロックをアクティブにする
            if (-1 != PosNearListPos(moveList[f]) - 1)
            {
                GroundBloc.objList[PosNearListPos(moveList[f]) - 1].SetActive(false);
            }
        }

        //数値を消す処理
        if (moveList.Count != 0)
        {
            if (moveList[0] > lostPoint)
            {
                moveList.RemoveAt(0);
            }
        }
    }

    //数値を動かす処理
    void Move()
    {


    }

    //ブロックに一番近い数値を出す
    int PosNearListPos(float move)
    {
        var min = blockPosZ.Min(c => Math.Abs(c - move));
        int num = blockPosZ.IndexOf(blockPosZ.First(c => Math.Abs(c - move) == min));
        return num;
    }
}
