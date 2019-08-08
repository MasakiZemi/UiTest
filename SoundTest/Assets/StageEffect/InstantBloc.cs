using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantBloc : MonoBehaviour
{

    public GameObject groundBloc;

    public Material material1;
    public Material material2;

    public int side;
    public int length;

    float x, z;
    float timer;

    int num;

    public List<GameObject> blocs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトの生成
        if (blocs.Count < side * length)
        {
            timer += 1.0f * Time.deltaTime;
            for (int i = 0; i < length; i++)
            {
                if (timer > 0.05f)
                {
                    x = -0.8f;
                    for (int j = 0; j < side; j++)
                    {
                        x += 0.4f;
                        blocs.Add(Instantiate(groundBloc, new Vector3(x, 0, z), Quaternion.Euler(0, 0, 0)) as GameObject);
                    }
                    z += 0.4f;
                    timer = 0;
                }
            }
        }
        else
        {

            //色を変える
            if (Music.IsPlaying && Music.IsJustChangedBar())
            {
                num = (num + 1) % 2;

                for (int i = 0; i < blocs.Count; i++)
                {
                    if (i % 2 == num)
                    {
                        blocs[i].transform.GetChild(0).GetComponent<Renderer>().material = material1;
                    }
                    else
                    {
                        blocs[i].transform.GetChild(0).GetComponent<Renderer>().material = material2;
                    }
                }
            }

            //


        }
    }
}
