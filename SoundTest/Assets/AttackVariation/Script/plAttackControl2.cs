using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plAttackControl2 : MonoBehaviour
{

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
    PlAttackAction.RollSwordParameter rollSword = new PlAttackAction.RollSwordParameter();

    // Start is called before the first frame update
    void Start()
    {
        plAct.melodyList = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        // 入力した番号を保存
        if (Music.IsPlaying && Music.IsJustChangedBar())
        {           
            //初期化
            plAct.Refresh();

            for (int i = 0; i < plAct.melodyList.Count; i++)
            {
                switch (plAct.melodyList[i])
                {
                    //攻撃
                    case 3:
                    case 4:
                    case 5:
                        plAct.attackStep++;
                        break;

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

                    default: break;
                }
            }

            //発動の式を書

            //スポーン
            PlAttackAction.onRollSwordSpawn = false;

            //発動に必要なフラグだけど、いらない気がする
            PlAttackAction.onRollSword = true;

            //発動する攻撃の種類の式を書く
            rollSword.swordCount = plAct.attackStep;


            .
            //すべて消す
            plAct.melodyList.Clear();

            //攻撃命令を出す
            actionTrigger = true;

        }

        //保存
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            plAct.melodyList.Add(HitPos.footPosNum);

            Debug.Log("aaa");
        }
    }
}
