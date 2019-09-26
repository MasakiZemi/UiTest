using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GameObject centerTarget;
    public GameObject obj;
    public float f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        obj.transform.position = CirclePos(0, obj);
    }
    Vector3 CirclePos(int count, GameObject funnelPos)
    {
        Vector3 pos = funnelPos.transform.position;
        float angle = f * Mathf.Deg2Rad;
        pos.x = Vector3.Distance(centerTarget.transform.position, funnelPos.transform.position) * Mathf.Cos(angle);
        pos.y = Vector3.Distance(centerTarget.transform.position, funnelPos.transform.position) * Mathf.Sin(angle);

        return pos;
    }
}
