using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlActionClass : MonoBehaviour
{
    enum RANK { Bad, Good, Excellent }
    struct PlayerAction
    {
        public List<int> melodyList;
        public int attackStep;
        public int defenseStep;
        public int supportStep;

        public void Refresh()
        {
            attackStep = 0;
            defenseStep = 0;
            supportStep = 0;
        }
    }
    PlayerAction plAct = new PlayerAction();

    bool actionTrigger;
    int barCount;
    float timer;


    // Start is called before the first frame update
    void Start()
    {
        plAct.melodyList = new List<int>();       
    }

    // Update is called once per frame
    void Update()
    {
        //入力した数字を攻撃、防御、サポートに分けカウントをする
        if (Music.IsPlaying && Music.IsJustChangedBar())
        {
            //初期化
            plAct.Refresh();

            //プレイヤーアクション格納システム
            for (int i = 0; i < plAct.melodyList.Count; i++)
            {
                switch (plAct.melodyList[i])
                {
                    //防御
                    case 0:
                    case 1:
                    case 7:
                        plAct.defenseStep++;
                        break;

                    //サポート
                    case 2:
                    case 6:
                        plAct.supportStep++;
                        break;

                    //攻撃
                    case 3:
                    case 4:
                    case 5:
                        plAct.attackStep++;
                        break;

                    default: break;
                }
            }

            //すべて消す
            plAct.melodyList.Clear();

            //攻撃命令を出す
            actionTrigger = true;
        }

        //入力した番号を保存
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            plAct.melodyList.Add(HitPos.footPosNum);
           
        }

        //遅延
        timer += 1.0f * Time.deltaTime;
        if (actionTrigger)
        {
            if (timer > 0.01f)
            {
                //攻撃処理
                if (0 < plAct.attackStep)
                {
                    Attack();
                    plAct.attackStep--;
                }

                //防御処理
                if (0 < plAct.defenseStep)
                {
                    Defense();
                    plAct.defenseStep--;
                }

                //サポート
                if (0 < plAct.supportStep)
                {
                    Support();
                    plAct.supportStep--;
                }

                //4回処理を行ったら処理を止める
                barCount++;
                timer = 0;
                if (barCount > 4) actionTrigger = false;
            }
        }

    }


    //
    public GameObject attackObj;

    //攻撃処理
    public void Attack()
    {
        Debug.Log("攻撃");

        //召喚位置の処理

    }

    //防御処理
    public void Defense()
    {
        Debug.Log("防御");
    }

    //サポート処理
    public void Support()
    {
        Debug.Log("サポート");
    }
}
