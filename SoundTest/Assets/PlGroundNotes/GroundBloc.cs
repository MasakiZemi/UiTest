using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBloc : MonoBehaviour
{
    public GameObject obj;
    public float space = 0.2f;
    public int count = 10;
    public static List<GameObject> objList = new List<GameObject>();

    private void Awake()
    {
        float sizZ = obj.transform.localScale.z;
        for (int i = 0; i < count; i++)
        {
            objList.Add(Instantiate(obj, new Vector3(0, 0, (space + sizZ) * i), new Quaternion()));
            objList[objList.Count - 1].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //public static List<GameObject> GetGroundBlock {get { return objList; } }

}
