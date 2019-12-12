using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///プレイヤーのステップ位置を操作するように作られたオブジェクト
/// テキストデータをもとにマテリアルを差し替えたり、データを格納したりする
/// マウスが持っているデータを受け取ったりもする
/// </summary>
public class ScoreObjPl : MonoBehaviour
{
    public bool onClick;
    public StepData.PL_STEP_TIMING plStepTiming;

    // Start is called before the first frame update
    void Start()
    {
        //テキストデータをもとに書き換える
        GameObject obj = PlModeGroup.obj;
        int num = (int)transform.localPosition.y;
        for (int i = 0; i < obj.transform.childCount - 1; i++)
        {
            StepData.PL_STEP_TIMING pl = StepData.GetStepData[num].plStep;
            if (pl == obj.transform.GetChild(i).gameObject.GetComponent<PlModeObj>().plModeType)
            {
                //データの取得
                plStepTiming = pl;
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
            GetComponent<Renderer>().material = MouseStatus.GetPlMaterial;
            plStepTiming = MouseStatus.GetPlStepTiming;
            onClick = false;
        }
    }
}
