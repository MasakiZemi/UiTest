using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineMove : MonoBehaviour
{
    public float speed = 10;
    public GameObject obj;
    List<GameObject> objList = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            objList.Add(Instantiate(obj, transform));
        }

        for(int i = 0; i < objList.Count; i++)
        {
            Vector3 pos = objList[i].transform.position;
            pos.z += speed * Time.deltaTime;
            objList[i].transform.position = pos;
        }
    }
}
