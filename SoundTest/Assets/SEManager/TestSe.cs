﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSe : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SE_Manager.SePlay(SE_Manager.SE_NAME.FootL);
        }
    }
}
