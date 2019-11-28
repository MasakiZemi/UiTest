using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBloc : MonoBehaviour
{
    public GameObject obj;
    public float space = 0.2f;
    public int count = 10;
    List<GameObject> objList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        float sizZ = obj.transform.localScale.z;
        for(int i = 0; i < count; i++)
        {
            objList.Add(Instantiate(obj, new Vector3(0, 0, (space+ sizZ) * i), new Quaternion()));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
