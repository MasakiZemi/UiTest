using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlAttackAction : MonoBehaviour
{
    public GameObject target;
    public float rollSpeed = 1000;
    public float speed = 1;
    public float waitTime = 1.7f;
    float timer;

    float brake = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //遅延
        timer += 1.0f * Time.deltaTime;
        if (timer>waitTime)
        {
            //transform.eulerAngles = new Vector3(0, 0, 0);


            transform.LookAt(target.transform);



            if (timer > waitTime + 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.fixedTime * Time.fixedTime);
            }
        }
        else
        {
            brake++;
            transform.Rotate(rollSpeed - brake, 0, 0);
        }
        
    }
}
