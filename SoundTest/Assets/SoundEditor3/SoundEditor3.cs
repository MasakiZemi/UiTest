using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundEditor3 : MonoBehaviour
{
    public GameObject EditorUi;
    public GameObject content;
    public GameObject scrollView;
    public float space = 35;

    int activeUi;
    int count;

    List<GameObject> objList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        float siz = space * StepData.GetStepData.Count + 2;
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, siz);

        //float activeUiNum = scrollView.GetComponent<RectTransform>().sizeDelta.y / space;
        //float activeUi = activeUiNum / siz;

        activeUi = (int)(scrollView.GetComponent<RectTransform>().sizeDelta.y / space) + 1;

        for (int i = 0; i < StepData.GetStepData.Count; i++)
        {
            objList.Add(Instantiate(EditorUi, content.transform));
            objList[i].GetComponent<RectTransform>().localPosition = new Vector3(0, -1 * i * space + 10, 0);
            objList[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (content.GetComponent<RectTransform>().localPosition.y > space * (count + 1))
        {
            objList[count].SetActive(false);
            count++;
        }
        if (content.GetComponent<RectTransform>().localPosition.y < space * count)
        {
            if (count != 0)
            {
                objList[count + activeUi].SetActive(false);
                count--;
            }
        }

        for (int i = count; i < activeUi + count; i++)
        {
            objList[i].SetActive(true);
        }
    }
}
