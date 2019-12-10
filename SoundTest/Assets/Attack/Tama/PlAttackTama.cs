using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlAttackTama : MonoBehaviour
{
    public GameObject enemyPos;
    public GameObject instantPos;
    public GameObject instantObj;
    public float speed = 10;
    public int actionStartNum = 5;
    
    class Attack
    {
        public GameObject obj;
        public bool onStart;
    }
    List<Attack> attackList = new List<Attack>();

    int triggerCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //トリガーが押された場合オブジェクトを発生させる
        if (OnTrigger())
        {
            InstantObj(instantPos.transform.position);

            triggerCount++;
        }

        //トリガーが特定の回数押された場合
        if (triggerCount == actionStartNum)
        {
            //チェックがついていないものを探す
            for (int i = 0; i < attackList.Count; i++)
            {
                if (!attackList[i].onStart)
                {
                    attackList[i].onStart = true;
                }
            }

            //カウントを0に
            triggerCount = 0;
        }

        MoveObj(enemyPos.transform.position);

        DestroyObj(enemyPos.transform.position);
    }

    #region オブジェクトコントロール
    //オブジェクトの生成
    void InstantObj(Vector3 pos)
    {
        if (triggerCount == 0)
        {
            attackList.Add(new Attack());
            attackList[attackList.Count - 1].obj = Instantiate(instantObj, pos, new Quaternion());
        }
    }
    //オブジェクトを動かす
    void MoveObj(Vector3 pos)
    {
        for (int i = 0; i < attackList.Count; i++)
        {
            //チェックがついているオブジェクトを動かす
            if (attackList[i].onStart)
            {
                attackList[i].obj.transform.position =
                    Vector3.MoveTowards(attackList[i].obj.transform.position, pos, speed * Time.deltaTime);
            }
        }
    }
    //敵座標までたどり着いた場合オブジェクトを破棄する
    void DestroyObj(Vector3 pos)
    {
        if (attackList.Count != 0)
        {
            if (Vector3.Distance(attackList[0].obj.transform.position, pos) < 0.1f)
            {
                Destroy(attackList[0].obj);
                attackList.RemoveAt(0);
            }
        }
    }
    #endregion

    //ダメージを管理
    float DamageControl()
    {
        float damage;
        damage = 10;

        return damage;
    }

    //トリガーを管理
    bool OnTrigger()
    {
        bool trigger = Input.GetKeyDown(KeyCode.Space);
        return trigger;
    }
}
