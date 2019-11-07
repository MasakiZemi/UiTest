using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleMove : MonoBehaviour
{
    public int instantCount;
    public GameObject instantObj;
    public float rad;

    public int divide;
    public float maxSize;
    public float minSize;

    List<GameObject> objList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        objList = new List<GameObject>(InstantCirclePos(instantCount, instantObj, rad));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 SizChange(float maxSiz, float minSaz,int cutNum,int sizChangeCount)
    {
        //float fixSiz = sizChangeCount / cutNum;

        return Vector3.zero;
    }

    //半円上にオブジェクトを生成する
    List<GameObject> InstantCirclePos(int count, GameObject obj, float radius)
    {
        List<GameObject> objList = new List<GameObject>();
        for (int i = 0; i < count; i++)
        {
            //半円上に生成する
            Vector3 v3 = CirclePos(count, radius, i, Vector3.zero);
            Quaternion q = Quaternion.LookRotation(Vector3.up, transform.position - v3);
            objList.Add(Instantiate(obj, v3, q * Quaternion.AngleAxis(90, Vector3.right)));
        }
        return objList;

        //半円上のポジションを取得
        Vector3 CirclePos(int maxNum, float rad, int currentNum, Vector3 pos)
        {
            if (maxNum != 0)
            {
                //きれいに半円状にに出すやつ
                float r = (360 / maxNum) * currentNum;

                float angle = r * Mathf.Deg2Rad;
                pos.x = rad * Mathf.Cos(angle);
                pos.z = rad * Mathf.Sin(angle);
            }
            else
            {
                pos.x = 0;
                pos.z = rad;
            }

            return pos;
        }
    }
}
