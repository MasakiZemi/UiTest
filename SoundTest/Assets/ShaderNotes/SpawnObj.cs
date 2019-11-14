using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObj : MonoBehaviour
{
    public GameObject obj;
    public Vector3 destPos = new Vector3(0, 0, 10);
    public float speed = 5;

    public Material material;

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

        for (int i = 0; i < objList.Count; i++)
        {
            if (objList[i].transform.position.z < destPos.z)
            {
                Vector3 pos = objList[i].transform.position;
                pos.z += speed * Time.deltaTime;
                objList[i].transform.position = pos;

                //material.SetFloat("_NotesPos" + i, pos.z);
            }
            else
            {
                Destroy(objList[i]);
                objList.RemoveAt(i);
            }
        }
        //if (objList.Count != 0)
        //{
        //    material.SetFloat("Vector1_E0D2F8C3", objList[0].transform.position.z);
        //    //Debug.Log(material.GetVector("_NotesPos"));
        //}
        //material.SetVectorArray("_NotesPos", v4);
    }
}
