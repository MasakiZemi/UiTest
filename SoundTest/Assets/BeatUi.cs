using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatUi : MonoBehaviour
{

    public GameObject notes;
    public GameObject notes1;
    public GameObject canvas;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Music.IsPlaying && Music.IsJustChangedBeat())
        {
            GameObject prefab = Instantiate(notes) as GameObject;
            prefab.transform.SetParent(canvas.transform, false);

            GameObject prefab1 = Instantiate(notes1) as GameObject;
            prefab1.transform.SetParent(canvas.transform, false);
        }
    }
}
