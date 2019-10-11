using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanShaderTiming : MonoBehaviour
{

    public Material material;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Music.IsPlaying)
        {
            material.SetFloat("Vector1_156873A5", 1f);


            //光の棒が出るまでの時間を調整したい（1小節ごとに）
            //現在はできていない
            material.SetFloat("Vector1_C34C010D", 2f);
        }
        //Debug.Log(material.GetInt("MusicTiming"));
    }
}
