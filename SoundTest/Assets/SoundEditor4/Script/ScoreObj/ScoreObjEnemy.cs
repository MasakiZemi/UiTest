using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObjEnemy : MonoBehaviour
{
    public bool onClick;
    public StepData.ENEMY_ATTACK_TYPE enemyAttackType;

    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = EnemyModeGroup.obj;
        int num = (int)transform.localPosition.y;
        for(int i=0; i < obj.transform.childCount - 1;i++)
        {
            StepData.ENEMY_ATTACK_TYPE enemy = StepData.GetStepData[num].ememyAttackType;
            if (enemy == obj.transform.GetChild(i).gameObject.GetComponent<EnemyModeObj>().enemyModeType)
            {
                enemyAttackType = enemy;
                GetComponent<Renderer>().material = obj.transform.GetChild(i).gameObject.GetComponent<Renderer>().material;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onClick)
        {
            GetComponent<Renderer>().material = MouseStatus.GetEnemyMaterial;
            enemyAttackType = MouseStatus.GetEnemyAttackType;
            onClick = false;
        }
    }
}
