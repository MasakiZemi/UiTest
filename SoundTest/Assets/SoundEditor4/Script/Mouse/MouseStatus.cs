﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// レイを飛ばしてオブジェクトの情報を取得。
/// 特定のオブジェクトをクリックした場合、取得したデータをそのオブジェクトに渡す
/// </summary>
public class MouseStatus : MonoBehaviour
{
    //プレイヤーの譜面に渡すよう
    public static StepData.PL_STEP_TIMING GetPlStepTiming { get; private set; }
    public static Material GetPlMaterial { get; private set; }
    public static Vector3 GetPlModePos { get; private set; }

    //敵の譜面に渡すよう
    public static StepData.ENEMY_ATTACK_TYPE GetEnemyAttackType { get; private set; }
    public static Material GetEnemyMaterial { get; private set; }
    public static Vector3 GetEnemyModePos { get; private set; }

    //デフォルトのデータをセットするためのやつ
    public GameObject plModeDefault;
    public GameObject enemyModeDefault;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        GetPlStepTiming = StepData.PL_STEP_TIMING.Nothing;
        GetPlMaterial = plModeDefault.GetComponent<Renderer>().material;
        GetPlModePos = plModeDefault.transform.position;

        GetEnemyAttackType = StepData.ENEMY_ATTACK_TYPE.Nothing;
        GetEnemyMaterial= enemyModeDefault.GetComponent<Renderer>().material;
        GetEnemyModePos= enemyModeDefault.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            //マウスクリックした場所からRayを飛ばし、オブジェクトがあればtrue 
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                GameObject obj = hit.collider.gameObject;

                //クリックしたものによって受け渡すデータを変える
                switch (obj.tag)
                {
                    case "EnemyMode":
                        GetEnemyAttackType = obj.GetComponent<EnemyModeObj>().enemyModeType;
                        GetEnemyMaterial = obj.GetComponent<Renderer>().material;
                        GetEnemyModePos = obj.transform.position;
                        break;

                    case "PlMode":
                        GetPlStepTiming = obj.GetComponent<PlModeObj>().plModeType;
                        GetPlMaterial = obj.GetComponent<Renderer>().material;

                        GetPlModePos = obj.transform.position;
                        break;

                    case "ScoreObjBool":
                        obj.GetComponent<ScoreObjBool>().onClick = true;
                        break;

                    case "ScoreObjEnemy":
                        obj.GetComponent<ScoreObjEnemy>().onClick = true;
                        break;

                    case "ScoreObjPl":
                        obj.GetComponent<ScoreObjPl>().onClick = true;
                        break;

                    default:break;
                }
            }
        }
    }

}
