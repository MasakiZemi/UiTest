using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//両サイド作ってから判定の処理を書くこと

public class NotesManager : MonoBehaviour
{
    public GameObject notesObj;
    public float notesPointRoll;
    public float speed;
    float move;

    List<GameObject> notesObjList=new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //notesObjList.Add(Instantiate(notesObj, transform));
    }

    // Update is called once per frame
    void Update()
    {
        SpawnNotes();
        NotesMove();
    }

    void SpawnNotes()
    {
        if(Music.IsPlaying)
        {
            if (Music.IsJustChangedBar())
            {
                notesObjList.Add(Instantiate(notesObj, transform));
            }
            else if (Music.IsJustChangedBeat())
            {
                notesObjList.Add(Instantiate(notesObj, transform));
                //notesObjList[notesObjList.Count].GetComponent<Renderer>().material.color = Color.red;
            }
        }
    }

    void NotesMove()
    {
        for (int i = 0; i < notesObjList.Count; i++)
        {
            Vector3 v3 = notesObjList[i].transform.rotation.eulerAngles;
            v3.y += speed * Time.deltaTime;
            notesObjList[i].transform.rotation = Quaternion.Euler(v3);
        }
    }
}
