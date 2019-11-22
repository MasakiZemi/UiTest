using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = worldPos;
    }

   

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0)) //マウスがクリックされたら
        {
            GameObject obj;
            switch (other.gameObject.tag)
            {
                case "True":
                    obj = GetObj(other.gameObject, 0);
                    other.gameObject.SetActive(false);
                    obj.SetActive(true);
                    break;

                case "False":
                    obj = GetObj(other.gameObject, 1);
                    other.gameObject.SetActive(false);
                    obj.SetActive(true);
                    break;

                case "AttNothing":


                    break;
                default: break;
            }
        }
    }

    GameObject GetObj(GameObject obj, int childNum)
    {

        int num = (int)obj.transform.position.x;
        GameObject parentObj = obj.transform.parent.gameObject.transform.parent.gameObject;
        GameObject childObj = parentObj.transform.GetChild(childNum).gameObject.transform.GetChild(num).gameObject;
        Debug.Log(parentObj.name);

        return childObj;
    }

}
