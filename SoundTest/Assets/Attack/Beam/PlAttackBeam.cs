using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlAttackBeam : MonoBehaviour
{
    public GameObject targetObj;
    public GameObject beamObj;
    public float speed = 5;
    public float rad = 5;
    public int spawnCount = 4;

    List<GameObject> beamObjList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OnTrigger())
        {
            beamObjList = new List<GameObject>(InstantCirclePos(spawnCount, beamObj, rad));
        }

        foreach(GameObject obj in beamObjList)
        {
            Vector3 pos = obj.transform.position;
            pos = Vector3.MoveTowards(pos, targetObj.transform.position, speed * Time.deltaTime);
            obj.transform.LookAt(targetObj.transform);
            obj.transform.position = pos;
        }
    }

    //半円状にオブジェクトを生成
    List<GameObject> InstantCirclePos(int count, GameObject obj, float radius)
    {
        List<GameObject> objList = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            //半円上に生成する
            Vector3 v3 = CirclePos(count - 1, radius, i, Vector3.zero);
            objList.Add(Instantiate(obj, v3, new Quaternion()));
        }
        return objList;

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
