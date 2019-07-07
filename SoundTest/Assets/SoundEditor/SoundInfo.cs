using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class SoundInfo : MonoBehaviour
{
    AudioSource source;
    AudioClip clip;

    public string title = "test1";

    public Slider timeSlider;
    bool onMusic = true;

    public List<float> musicScore = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        clip = source.clip;

        //再生バーの終了位置セット
        timeSlider.maxValue = clip.length;



        //キャスト(.txtをstring型の配列で受け取り、string型の配列をfloat型の配列に直し、float型の配列をfloat型のListに直す)
        musicScore.AddRange(Array.ConvertAll(File.ReadAllLines("aaa.txt"), float.Parse));

        //str = File.ReadAllLines("aaa.txt");
        //musicScore_.AddRange(Array.ConvertAll<string, float>(File.ReadAllLines("aaa.txt"), delegate (string value)
        //{
        //    return float.Parse(value);
        //}));
        //musicScore.AddRange(File.ReadAllLines("aaa.txt"));
    }

    // Update is called once per frame
    void Update()
    {
        //ソート
        musicScore.Sort();

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

        //Rを押したら時間を記録する
        if (Input.GetKeyDown(KeyCode.R))
        {
            musicScore.Add(source.time);
        }

        //Sを押したら保存する
        if (Input.GetKeyDown(KeyCode.S))
        {
            //キャスト　(float型のListをstring型の入るつに変換)
            string[] str = Array.ConvertAll(musicScore.ToArray(), delegate (float value)
             {
                 return value.ToString();
             });

            //テキストに書き出し
            File.WriteAllLines("aaa.txt", str);
        }

        //消す
        if (musicScore.Contains(0)) musicScore.Remove(0);

    }
}
