using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlAttackAction : MonoBehaviour
{
    
    float timer;
    //float r;
    public GameObject centerTarget;

    public static bool onRollSword { get; set; }
    public static bool onRollSwordSpawn { get; set; }


    //くるくると回ってからターゲットに向かって放たれる
    [System.Serializable]
    public class RollSwordParameter
    {
        public GameObject target;
        public GameObject swordObj;
        public float rollSpeed = 25;
        public float speed = 5;
        public float waitTime = 0.5f;
        public int swordCount = 4;

        [Header( "使わないやつ")]
        public List<GameObject> swordList = new List<GameObject>();
        public float brake = 1;
        public bool onSword;
    }
    public RollSwordParameter RSP = new RollSwordParameter();


    public class CrossSwordParameter
    {
        public GameObject target1;
    }
    //public RollSwordParameter CSR = new RollSwordParameter();

    //王の宝物庫
    [System.Serializable]
    public class GateOfBabylonParameter
    {
        public GameObject target;
        public GameObject gateObj;
        public GameObject swordObj;
        public int swordCount;
        public float speed;

        public List<GameObject> gate = new List<GameObject>();
        public List<GameObject> sword = new List<GameObject>();

        public bool onGate;
        public bool onSword;
    }
    public GateOfBabylonParameter GOBP = new GateOfBabylonParameter();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (onRollSword)
        {
            RollSword();

            //再生成防ぐやつ
            GOBP.onGate = onRollSwordSpawn;
        }
    }

    //途中
    void GateOfBabylon()
    {
        //遅延
        timer += 1.0f * Time.deltaTime;

        if (!GOBP.onGate)
        {
            for (int i = 0; i < RSP.swordCount; i++)
            {
                //距離の変数を用意すること
                Vector3 v3 = CirclePos(GOBP.swordCount, new Vector3(50, 50, 0), i);
                GOBP.gate.Add(Instantiate(GOBP.swordObj, v3, new Quaternion()));
            }

            //無限生成防ぐやつ
            GOBP.onGate = true;
        }
    }

    //くるくると回ってからターゲットに向かって放たれる
    public void RollSword()
    {
        float wait = 0.5f;

        //遅延
        timer += 1.0f * Time.deltaTime;

        //オブジェクト生成
        if (!RSP.onSword)
        {
            for (int i = 0; i < RSP.swordCount; i++)
            {
                //距離の変数を用意すること
                Vector3 v3 = CirclePos(RSP.swordCount, new Vector3(50, 50, 0), i);
                RSP.swordList.Add(Instantiate(RSP.swordObj, v3, new Quaternion()));
            }
            RSP.onSword = true;
        }

       //生成した数分だけ操作する
        foreach (GameObject sword in RSP.swordList)
        {
            wait +=0.2f ;

            if (timer > RSP.waitTime)
            {
                //ターゲットの方向を見る
                sword.transform.LookAt(RSP.target.transform);

                if (timer > RSP.waitTime + wait)
                {
                    //飛んでいく
                    sword.transform.position = Vector3.MoveTowards(sword.transform.position, RSP.target.transform.position, RSP.speed * Time.fixedTime * Time.fixedTime);
                }
            }
            else
            {
                //初めの回転演出
                RSP.brake++;
                sword.transform.Rotate(RSP.rollSpeed, 0, 0);
            }
        }
    }

    //配置用
    Vector3 CirclePos(int count, Vector3 pos, int swordNum)
    {
        float r = 180 / count * swordNum;
        //float r = 360 / count * swordNum;
        //float angle = r * Mathf.Deg2Rad;
        float angle = r ;
        pos.x = Vector3.Distance(centerTarget.transform.position, pos) * Mathf.Cos(angle);
        pos.y = Vector3.Distance(centerTarget.transform.position, pos) * Mathf.Sin(angle);
        return pos;
    }

}
