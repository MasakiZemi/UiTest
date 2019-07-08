using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExHitObj : MonoBehaviour
{
    public static bool isExHit { get; set; }
    float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (isExHit) timer += 1.0f * Time.deltaTime;

        //if (timer > 1f)
        //{
        //    isExHit = false;
        //    timer = 0;
        //}

        Debug.Log(isExHit);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Notes") isExHit = true;
    }
}
