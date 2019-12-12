using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Manager : MonoBehaviour
{
    AudioSource[] seArr;

    public enum SE_NAME
    {
        FootR, FootL, FootLR,
        PlDamage, PlAttack
    }

    [System.Serializable]
    public class SeData
    {
        [HideInInspector] public string name;
        public AudioClip audio;
    }
    public List<SeData> seDataList = new List<SeData>()
    {
        //リストの初期化
        new SeData{name=SE_NAME.FootR.ToString(),audio=null},
        new SeData{name=SE_NAME.FootL.ToString(),audio=null},
        new SeData{name=SE_NAME.FootLR.ToString(),audio=null},

        new SeData{name=SE_NAME.PlDamage.ToString(),audio=null},
        new SeData{name=SE_NAME.PlAttack.ToString(),audio=null}
    };

    static SE_Manager SE_Manager_;

    // Start is called before the first frame update
    void Start()
    {
        //オーディオをアタッチする
        for (int i = 0; i < 4; i++)
        {
            gameObject.AddComponent<AudioSource>();
        }
        seArr = GetComponents<AudioSource>();

        SE_Manager_ = this;
    }

    // Update is called once per frame
    void Update()
    {
    }

    /// <summary>
    /// SEの再生、どのSEを使うか選択して！
    /// </summary>
    /// <param name="seType"></param>
    public static void SePlay(SE_NAME seType)
    {
        //被って流せる音は最大4つまで
        //再生中でないオーディオを探す
        foreach (AudioSource se in SE_Manager_.seArr)
        {
            if (!se.isPlaying)
            {
                se.PlayOneShot(SE_Manager_.seDataList[(int)seType].audio);
                break;
            }
        }
    }
}
