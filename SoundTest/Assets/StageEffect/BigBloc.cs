using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBloc : MonoBehaviour
{
    public GameObject bloc;
    public int num;
    public 

    List<GameObject> blocs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < num; i++)
        {
            blocs.Add(Instantiate(bloc, transform));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
