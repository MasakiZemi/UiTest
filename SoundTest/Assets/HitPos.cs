using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPos : MonoBehaviour
{
    public List<GameObject> listObj = new List<GameObject>();
    float notesLeftPos;
    float notesRightPos;

    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //listObj = BeatUi.notesLefts;
        notesLeftPos = BeatUi.notesLefts[0].GetComponent<RectTransform>().localPosition.x;
        notesRightPos = BeatUi.notesRights[0].GetComponent<RectTransform>().localPosition.x;

        //バグありリストをどうにかして直す

        //Debug.Log(BeatUi.notesLefts[0].name);
        if (notesLeftPos >= -150f && notesLeftPos < 1f)
        {
            obj = BeatUi.notesLefts[0];

            if (Input.GetKeyDown(KeyCode.Space))
            {
                BeatUi.notesLefts.Remove(obj);
                if (notesLeftPos <= 0f && notesLeftPos >= -30f)
                {
                    Debug.Log("Excellent!!");
                    Destroy(obj);
                }
                if (notesLeftPos < -30f && notesLeftPos >= -60f)
                {
                    Debug.Log("Good!!");
                    Destroy(obj);
                }
                if (notesLeftPos < -60f && notesLeftPos >= -150f)
                {
                    Debug.Log("Bad!!");
                    Destroy(obj);
                }
            }
        }
        else
        {
            BeatUi.notesLefts.Remove(obj);
            Destroy(obj);
        }

        //if (notesRightPos > -100f)
        //{
        //    GameObject obj1 = BeatUi.notesRights[0];
        //    BeatUi.notesRights.RemoveAt(0);
        //    Destroy(obj1);
        //}
    }
}
