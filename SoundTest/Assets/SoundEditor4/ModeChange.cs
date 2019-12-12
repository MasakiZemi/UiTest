using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeChange : MonoBehaviour
{
    public GameObject enemyModeGroup;
    public GameObject plModeGroup;

    List<Material> enemyModeObj = new List<Material>();
    List<Material> plModeObj = new List<Material>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < enemyModeGroup.transform.childCount; i++)
        {
            enemyModeObj.Add(enemyModeGroup.transform.GetChild(i).gameObject.GetComponent<Renderer>().material);
        }
        for (int i = 0; i < plModeGroup.transform.childCount; i++)
        {
            plModeObj.Add(plModeGroup.transform.GetChild(i).gameObject.GetComponent<Renderer>().material);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
