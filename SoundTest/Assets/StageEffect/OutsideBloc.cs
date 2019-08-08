using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideBloc : MonoBehaviour
{
    public GameObject[] outsideBlocs;
    public float speed = 5;
    public float rollSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //回転
        if (transform.rotation.eulerAngles.z < 45)
        {
            transform.Rotate(0, 0, rollSpeed * Time.deltaTime);
        }

        //サイズ
        if (outsideBlocs[0].transform.localScale.x < 25)
        {
            speed += speed * Time.fixedTime * Time.fixedTime;
            for (int i = 0; i < outsideBlocs.Length; i++)
            {
                outsideBlocs[i].transform.localScale = new Vector3(speed, 1, 1);
            }
        }
    }
}
