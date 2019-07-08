using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodHitObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        ExHitObj.isExHit = false;
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Notes")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (ExHitObj.isExHit)
                {
                    Debug.Log("Excellent!!");
                }
                else
                {
                    Debug.Log("Good!");
                }
            }

        }
    }
}
