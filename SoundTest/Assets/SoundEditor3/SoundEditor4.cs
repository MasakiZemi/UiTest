using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SoundEditor4 : MonoBehaviour
{
    public GameObject EditorUi;
    public GameObject content;
    public GameObject scrollView;
    public float space = 35;

    public int activeUi;
    public int count;

    List<GameObject> objList = new List<GameObject>();
    List<float> objPos = new List<float>();

    // Start is called before the first frame update
    void Start()
    {

        float siz = space * StepData.GetStepData.Count + 2;

        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, siz);

        //float activeUiNum = scrollView.GetComponent<RectTransform>().sizeDelta.y / space;
        //float activeUi = activeUiNum / siz;

        //表示する数の計算
        activeUi = (int)(scrollView.GetComponent<RectTransform>().sizeDelta.y / space) + 1;

        //UIの表示
        for (int i = 0; i < StepData.GetStepData.Count; i++)
        {
            objList.Add(Instantiate(EditorUi, content.transform));
            objList[i].GetComponent<RectTransform>().localPosition = new Vector3(0, -1 * i * space + 10, 0);
            objPos.Add(-1 * i * space + 10);
            objList[i].SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        //マウスのボタンから話した場合高速計算
        if (Input.GetMouseButtonUp(0))
        {
            int a = count;
            for (int i = 0; i < StepData.GetStepData.Count; i++)
            {
                Teleport();

                if (a == count) break;
                else a = count;
            }
        }

        //マウスのボタンが押されている場合計算をやめる
        if (!Input.GetMouseButton(0))
        {
            Teleport();
        }

    }

    void Teleport()
    {
        //進んだ場合前のやつを非表示にする
        if (content.GetComponent<RectTransform>().localPosition.y > space * (count + 1))
        {
            objList[count].SetActive(false);
            count++;
        }

        //戻った場合後のやつを非表示にする
        if (content.GetComponent<RectTransform>().localPosition.y < space * count)
        {
            if (count != 0)
            {
                objList[count + activeUi].SetActive(false);
                count--;
            }
        }

        //アクティブのフラグ管理
        for (int i = count; i < activeUi + count; i++)
        {
            objList[i].SetActive(true);
        }
    }
}
