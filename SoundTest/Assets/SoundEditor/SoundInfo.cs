using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Xml;

public class SoundInfo : MonoBehaviour
{
    AudioSource source;
    AudioClip clip;

    public string title = "test1";

    public Slider timeSlider;
    bool onMusic = true;

    public static List<float> musicScore = new List<float>();
    public  List<float> musicScore_ = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        clip = source.clip;

        timeSlider.maxValue = clip.length;
    }

    // Update is called once per frame
    void Update()
    {
        //BGMの再生時間と再生バーをリンク
        if (onMusic) timeSlider.value = source.time;

        //マウスクリックをするとBGMが止まる
        if (Input.GetMouseButtonDown(0))
        {
            source.Stop();
            onMusic = false;
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            source.time = timeSlider.value; //再生バーの位置とBGM再生位置をリンク
            onMusic = true;
            source.Play();                  //BGM再生
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            musicScore.Add(source.time);
            musicScore_.Add(source.time);
        }


    }
}
