using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShaderNotesControl1 : MonoBehaviour
{

    public float notesSpeed = 5;
    //public GameObject destroyPos;
    public GameObject startPos;

    [Serializable]
    public class Shader
    {
        public Material material;
        public GameObject stageObj;
        public List<int> numList = new List<int>();
        public bool[] onMove = new bool[4];

        public float StartPosZ() { return stageObj.transform.Find("Start").gameObject.transform.position.z * -1; }
        public float DestroyPosZ() { return stageObj.transform.Find("Destroy").gameObject.transform.position.z * -1; }
    }
    public List<Shader> shadersList = new List<Shader>();

    class Notes
    {
        public float moveData;

    }
    List<Notes> notesList = new List<Notes>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++) shadersList[0].numList.Add(i);
    }

    // Update is called once per frame
    void Update()
    {
        NotesMove();
        SetShader();
        //Debug.Log(shadersList[0].DestroyPosZ());
    }

    void NotesMove()
    {
        if (OnTrigger())
        {
            notesList.Add(new Notes());
            notesList[notesList.Count - 1].moveData = startPos.transform.position.z * -1;
        }

        for (int i = 0; i < notesList.Count; i++)
        {
            notesList[i].moveData += notesSpeed * Time.deltaTime;
        }
    }

    void SetShader()
    {
        //複製ができない
        int count = 0;
        for (int i = 0; i < notesList.Count; i++)
        {
            //特定のエリアに入った場合
            if (notesList[i].moveData > shadersList[0].StartPosZ() &&
                notesList[i].moveData <= shadersList[0].DestroyPosZ())
            {
                //シェーダーへの代入
                shadersList[0].material.SetFloat("_Pos" + shadersList[0].numList[count], notesList[i].moveData);

                //配列のカウント
                if (count != 3) count++;
            }
            else
            {
                //位置のリセット
                shadersList[0].material.SetFloat("_Pos" + shadersList[0].numList[count], shadersList[0].StartPosZ());

                //位置をずらす
                int num = shadersList[0].numList[count];
                shadersList[0].numList.RemoveAt(count);
                shadersList[0].numList.Add(num);

                if (count != 0) count--;
            }

            //ノースの消去
            if(notesList[i].moveData > shadersList[shadersList.Count - 1].StartPosZ())
            {
                notesList.RemoveAt(i);
            }
        }

        


    }

    //トリガーの処理
    bool OnTrigger()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
