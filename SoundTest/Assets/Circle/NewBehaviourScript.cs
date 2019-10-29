using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject obj;
    public List<GameObject> objList = new List<GameObject>();
    public int count = 50;
    public float range = 5;
    public float siz = 0.1f;
    
    class RangeSiz
    {
        public int percentage;
        public Vector2 siz;     //xが最大値　yが最小値
    }

    // Start is called before the first frame update
    void Start()
    {
        objList = new List<GameObject>(InstantCirclePos(count, obj, range));

        for(int i = 0; i < objList.Count; i++)
        {
            objList[i].transform.parent = transform;
            objList[i].transform.localScale = new Vector3(siz, 1, siz);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
            objList.Add(Instantiate(obj, v3, q*Quaternion.AngleAxis(90, Vector3.right)));
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
