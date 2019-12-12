using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UIの選択している位置がわかるようにするやつ
/// </summary>
public class ModeSymbol : MonoBehaviour
{
    public enum Mode { Pl,Enemy}
    public Mode mode;
    public GameObject symbolObj;
    Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //クリックしたときに取得した座標データをもとに、位置を動かす
        switch (mode)
        {
            case Mode.Pl:
                pos = symbolObj.transform.position;
                pos.x = MouseStatus.GetPlModePos.x;
                pos.y = MouseStatus.GetPlModePos.y;
                symbolObj.transform.position = pos;
                break;

            case Mode.Enemy:
                pos = symbolObj.transform.position;
                pos.x = MouseStatus.GetEnemyModePos.x;
                pos.y = MouseStatus.GetEnemyModePos.y;
                symbolObj.transform.position = pos;
                break;

            default: break;
        }
    }
}
