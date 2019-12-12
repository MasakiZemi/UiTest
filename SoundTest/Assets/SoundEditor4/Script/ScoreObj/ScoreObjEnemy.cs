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
        enemyAttackType = StepData.ENEMY_ATTACK_TYPE.Nothing;
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
