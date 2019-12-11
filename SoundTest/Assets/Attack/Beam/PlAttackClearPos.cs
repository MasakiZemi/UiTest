using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlAttackClearPos : MonoBehaviour
{
    public string targetName = "TargetPos";

    ParticleSystem par;
    Vector3 clearPos;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        clearPos = GameObject.Find(targetName).gameObject.transform.position;
        par = transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        //color = par.startColor.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (!par.isEmitting)
        {
            Destroy(gameObject);
        }
    }
}
