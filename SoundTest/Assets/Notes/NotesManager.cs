using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//両サイド作ってから判定の処理を書くこと

public class NotesManager : MonoBehaviour
{
    public GameObject notesObj;
    public float longNotesSiz=1.5f;
    public float speed;

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

                Vector3 v3 = notesObjList[notesObjList.Count - 1].transform.localScale;
                v3.y += longNotesSiz;
                notesObjList[notesObjList.Count - 1].transform.localScale = v3;
            }
            else if (Music.IsJustChangedBeat())
            {
                notesObjList.Add(Instantiate(notesObj, transform));
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
