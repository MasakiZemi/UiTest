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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Music.IsPlaying && Music.IsJustChangedBar())
        {
            //初期化
            plAct.Refresh();
        }


        // 入力した番号を保存
        if (Music.IsPlaying && Music.IsJustChangedBeat())
        {
            plAct.melodyList.Add(HitPos.footPosNum);

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

            //すべて消す
            plAct.melodyList.Clear();

            //攻撃命令を出す
            actionTrigger = true;

        }
    }
}
