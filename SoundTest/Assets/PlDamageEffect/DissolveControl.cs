using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveControl : MonoBehaviour
{
    public Material material;
    public float speed = 0.2f;
    public bool onFadeOut;
    public bool onFadeIn;
    float gage = 2;
    // Start is called before the first frame update
    void Start()
    {
        material.SetFloat("_Gauge", gage);
    }

    // Update is called once per frame
    void Update()
    {
        if (onFadeOut)
        {
            if (0 < gage)
            {
                material.SetFloat("_Gauge", gage -= Time.deltaTime * speed);
            }
        }
        else if (onFadeIn)
        {
            if (2 > gage)
            {
                material.SetFloat("_Gauge", gage += Time.deltaTime * speed);
            }
        }
    }
}
