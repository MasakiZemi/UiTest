using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
