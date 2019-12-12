using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敵の攻撃位置の操作用に作られたオブジェクト
/// 敵の攻撃位置のデータによってマテリアルを差し替える
/// また、そのデータを格納する
/// </summary>
public class ScoreObjBool : MonoBehaviour
{
    public bool onClick;
    public Material changeMaterial;
    Material material;

    public bool on;

    void Awake()
    {
        material = GetComponent<Renderer>().material;
    }

    // Start is called before the first frame update
    void Start()
    {
        //テキストから得たデータをもとにマテリアルを変え、データを受け取る
        int x = (int)transform.localPosition.x;
        int y = (int)transform.localPosition.y;
        on = StepData.GetStepData[y].enemyAttackPos[x];

        if (on)
        {
            GetComponent<Renderer>().material = changeMaterial;
        }
        else
        {
            GetComponent<Renderer>().material = material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //クリックをした判定が返ってきた場合
        if (onClick)
        {
            if (GetComponent<Renderer>().material == material)
            {
                GetComponent<Renderer>().material = changeMaterial;
                on = true;
            }
            else
            {
                GetComponent<Renderer>().material = material;
                on = false;
            }
            onClick = false;
        }
    }
}
