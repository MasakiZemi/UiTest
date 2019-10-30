using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixRoll : MonoBehaviour
{
    public GameObject fixObj;
    public GameObject targetObj;
    bool onStart = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void LateUpdate()
    {
        if (onStart) a(); if (onStart) a();
    }

    void a()
    {
        GameObject groupObj = new GameObject("Group");  //子オブジェクト回転用
        fixObj.transform.parent = gameObject.transform;
        gameObject.transform.rotation = targetObj.transform.rotation;
        fixObj.transform.parent = targetObj.transform;
        Destroy(groupObj);
        onStart = false;
    }
}
