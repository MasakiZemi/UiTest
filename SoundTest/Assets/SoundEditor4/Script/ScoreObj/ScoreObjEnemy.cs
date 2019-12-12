using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の攻撃タイプを操作するように作られたオブジェクト
/// テキストデータをもとにマテリアルを差し替えたり、データを格納したりする
/// マウスが持っているデータを受け取ったりもする
/// </summary>
public class ScoreObjEnemy : MonoBehaviour
{
    public bool onClick;
    public StepData.ENEMY_ATTACK_TYPE enemyAttackType;

    // Start is called before the first frame update
    void Start()
    {
        //テキストデータをもとにマテリアルをあ差し替え、データを取得する
        GameObject obj = EnemyModeGroup.obj;
        int num = (int)transform.localPosition.y;
        for(int i=0; i < obj.transform.childCount - 1;i++)
        {
            StepData.ENEMY_ATTACK_TYPE enemy = StepData.GetStepData[num].ememyAttackType;
            if (enemy == obj.transform.GetChild(i).gameObject.GetComponent<EnemyModeObj>().enemyModeType)
            {
                //データの取得
                enemyAttackType = enemy;
                //マテリアルの差し替え
                GetComponent<Renderer>().material = obj.transform.GetChild(i).gameObject.GetComponent<Renderer>().material;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //クリックをした判定が返ってきた場合
        if (onClick)
        {
            //マウスが持っているデータをもとにマテリアルを差し替え、データを取得する
            GetComponent<Renderer>().material = MouseStatus.GetEnemyMaterial;
            enemyAttackType = MouseStatus.GetEnemyAttackType;
            onClick = false;
        }
    }
}
