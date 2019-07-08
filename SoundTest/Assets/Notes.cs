using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour
{
    RectTransform rect;

    public float speed = 50f;
    public bool RorL;

    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!RorL)
        {
            rect.localPosition += Vector3.right * speed * Time.deltaTime;
            if (0 < rect.localPosition.x)
            {
                //ExHitObj.isExHit = false;
                Destroy(gameObject,0.1f);
            }
        }
        if (RorL)
        {
            rect.localPosition += Vector3.left * speed * Time.deltaTime;
            if (0 > rect.localPosition.x)
            {
                //ExHitObj.isExHit = false;
                Destroy(gameObject,0.1f);
            }
        }

    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Space) && RorL)
        {
            if (other.gameObject.tag == "ExcellentHit")
            {
                Debug.Log("Excellent!!");
            }
            else if (other.gameObject.tag == "GoodHit")
            {
                Debug.Log("Good!!");
            }
        }


    }
}