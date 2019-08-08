using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBlocs : MonoBehaviour
{

    public GameObject ringBloc;
    public int ringBlocCount;

    public float speed = 3;
    public float rollSpeed = 3;
    public float size = 10;

    float timer;
    float sizeSpeed = 0.5f;
    int count;

    public class RingBlocClass
    {
        public GameObject ringBlocs;
        public float speed;
        public float sizeSpeed;
        public bool frg;
    }
    public List<RingBlocClass> RingList = new List<RingBlocClass>();




    // Start is called before the first frame update
    void Start()
    {
        //生成
        for (int i = 0; i < ringBlocCount; i++)
        {
            RingList.Add(new RingBlocClass());
            RingList[i].ringBlocs = Instantiate(ringBloc, transform) as GameObject;
            RingList[i].speed = speed;
            RingList[i].sizeSpeed = sizeSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //遅延
        timer += 1.0f * Time.deltaTime;
        if (timer > 0.05f)
        {
            if (count != ringBlocCount)
            {
                timer = 0;
                count++;
            }
        }

        //演出処理
        for (int i = 0; i < count; i++)
        {
            //速度の設定
            if (RingList[i].speed < 10000 || RingList[i].sizeSpeed < 10000)
            {
                RingList[i].speed += RingList[i].speed * Time.fixedTime * Time.fixedTime;
                RingList[i].sizeSpeed += RingList[i].sizeSpeed * Time.fixedTime * Time.fixedTime;
            }

            //回転
            if (i % 2 == 0)
            {
                RingList[i].ringBlocs.transform.Rotate(0, 0, rollSpeed * Time.deltaTime);
            }
            else
            {
                RingList[i].ringBlocs.transform.Rotate(0, 0, -rollSpeed * Time.deltaTime);
            }

            //サイズ
            if (RingList[i].ringBlocs.transform.lossyScale.x < size)
            {
                if (!RingList[i].frg)
                {
                    RingList[i].ringBlocs.transform.localScale = new Vector3(RingList[i].sizeSpeed, RingList[i].sizeSpeed, 1);
                }
            }
            else
            {
                RingList[i].ringBlocs.transform.localScale = new Vector3(size, size, 1);
                RingList[i].frg = true;
            }

            //移動
            if (RingList[i].ringBlocs.transform.localPosition.z > -((transform.position.z / ringBlocCount) * (i - ringBlocCount)))
            {
                RingList[i].ringBlocs.transform.localPosition = new Vector3(0, 0, -RingList[i].speed);
            }
            else
            {
                RingList[i].ringBlocs.transform.localPosition = new Vector3(0, 0, -((transform.position.z / ringBlocCount) * (i + 1)));
            }
        }

    }
}
