using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class ShaderNotesControl : MonoBehaviour
{
    public Material material;
    public float notesSpeed = 5;
    public GameObject destroyPos;
    public GameObject startPos;

    float[] setShaberArr = new float[4];
    bool[] onNotesMove = new bool[4];
    int getCount = 4;

    // Start is called before the first frame update
    void Start()
    {
        //ノーツの初期化
        for (int i = 0; i < getCount; i++)
        {
            setShaberArr[i] = startPos.transform.position.z * -1;
            material.SetFloat("_Pos" + i, setShaberArr[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //トリガーが入った場合フラグを立てる
        if (OnTrigger() && onNotesMove.Contains(false))
        {
            for (int i = 0; i < getCount; i++)
            {
                //trueになっていない配列を探す
                if (!onNotesMove[i])
                {
                    onNotesMove[i] = true;
                    break;  //見つけたらBreakする
                }
            }
        }

        //4つのノーツの移動
        for(int i = 0; i < getCount; i++)
        {
            if (setShaberArr[i] < destroyPos.transform.position.z * -1)
            {
                if (onNotesMove[i])
                {
                    //移動
                    setShaberArr[i] += notesSpeed * Time.deltaTime;

                    //シェーダーに渡す
                    material.SetFloat("_Pos" + i, setShaberArr[i]);
                }
            }
            else
            {
                //destroyPosを過ぎたらStartPosに戻す
                setShaberArr[i] = startPos.transform.position.z * -1;
                onNotesMove[i] = false;
            }
        }
    }

    //トリガーの処理
    bool OnTrigger()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
