﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlAttackBeam : MonoBehaviour
{
    public GameObject targetObj;
    public GameObject beamObj;
    public float speed = 5;
    public float rad = 5;
    public int spawnCount = 4;

    public List<GameObject> beamObjList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            for (int i = 0; i < spawnCount; i++)
            {
                beamObjList.Add(InstantCirclePos(i, spawnCount, beamObj, rad));
            }
        }

        foreach(GameObject obj in beamObjList)
        {
            Vector3 pos = obj.transform.position;
            pos = Vector3.MoveTowards(pos, targetObj.transform.position, speed * Time.deltaTime);
            obj.transform.LookAt(targetObj.transform);
            obj.transform.position = pos;
        }

        if (beamObjList.Count != 0)
        {
            float dis = Vector3.Distance(beamObjList[0].transform.position, targetObj.transform.position);
            if (1f > dis)
            {
                ParticleSystem par = beamObjList[0].transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
                if (!par.isEmitting)
                {
                    Destroy(beamObjList[0]);
                    beamObjList.RemoveAt(0);
                }
            }
        }
    }

    //半円状にオブジェクトを生成
    GameObject InstantCirclePos(int count, int maxCount, GameObject obj, float radius)
    {
        GameObject gameObject;
        //半円上に生成する
        Vector3 v3 = CirclePos(maxCount - 1, radius, count, Vector3.zero);
        gameObject = Instantiate(obj, v3, new Quaternion());
        gameObject.transform.parent = transform;
        return gameObject;

        //半円上のポジションを取得
        Vector3 CirclePos(int maxNum, float rad, int currentNum, Vector3 pos)
        {
            if (maxNum != 0)
            {
                //きれいに半円状にに出すやつ
                float r = (180 / maxNum) * currentNum;

                float angle = r * Mathf.Deg2Rad;
                pos.x = rad * Mathf.Cos(angle);
                pos.y = rad * Mathf.Sin(angle);
            }
            else
            {
                pos.x = 0;
                pos.y = rad;
            }

            return pos;
        }
    }

    bool OnTrigger()
    {
        return Input.GetKeyDown(KeyCode.Alpha3);
    }
}
