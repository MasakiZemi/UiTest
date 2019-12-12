using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreObjBool : MonoBehaviour
{
    public bool onClick;
    public Material changeMaterial;
    Material material;

    public bool on;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
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
