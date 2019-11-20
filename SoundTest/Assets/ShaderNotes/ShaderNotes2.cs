using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShaderNotes2 : MonoBehaviour
{
    public float speed = 10;
    public GameObject startPosObj;

    Material material;
    Vector3 startPos;
    Vector3 endPos;
    public float interval;
    bool onStart;

    [Serializable]
    public class Notes
    {
        public float timer;
        public bool onTimeStart;
        public float setShaderFloat;

        public Notes(float startPos)
        {
            timer = 0;
            onTimeStart = false;
            setShaderFloat = startPos;
        }
    }
    public Notes[] notesArr = new Notes[4];

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        startPos = startPosObj.transform.position;
        endPos = transform.GetChild(0).gameObject.transform.position;
        float dis = startPos.z - (transform.position.z + transform.localScale.z / 2);
        interval = startPos.z / speed;

        for (int i = 0; i < notesArr.Length; i++)
        {
            //notesArr[i] = new Notes(startPos);
            material.SetFloat("_Pos" + i, startPos.z * -1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            for(int i=0; i < notesArr.Length; i++)
            {
                if (!notesArr[i].onTimeStart)
                {
                    notesArr[i].onTimeStart = true;
                    break;
                }
            }
        }

        for(int i = 0; i < notesArr.Length; i++)
        {
            if (notesArr[i].onTimeStart)
            {
                notesArr[i].timer += Time.deltaTime;

                if (notesArr[i].timer > interval)
                {
                    notesArr[i].setShaderFloat += speed * Time.deltaTime;
                    material.SetFloat("_Pos" + i, notesArr[i].setShaderFloat);

                    if (notesArr[i].setShaderFloat >= endPos.z)
                    {
                        //notesArr[i] = new Notes();
                    }
                }
            }
        }

        
    }

    //トリガーの処理
    bool OnTrigger()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
