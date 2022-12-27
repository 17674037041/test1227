using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    public string name;//玩家名字
    public int killnum;//玩家死亡数
    public int strucknum;//玩家击杀数
}

public class Score : MonoBehaviour
{
    //private List<Player> player = new List<Player>();
    private Player[] player = new Player[5];
    //按钮
    private Button Player1Btn;
    private Button Player2Btn;
    private Button Player3Btn;
    private Button Player4Btn;
    private Button Player5Btn;

    //击杀和死亡文本
    private Text KillText;
    private Text StruckText;


    void Start()
    {
        Score_Sort();
    }
    // Start is called before the first frame update
    void Awake()
    {
        //实例化5个玩家 并初始化数据
        for(int i=0;i<5;i++)
        {
            player[i] = new Player();
            player[i].name = "玩家" + (i+1).ToString();
            player[i].killnum = 0;
            player[i].strucknum = 0;
        }

        //文本
        KillText = transform.Find("Panel_Kill/Text").GetComponent<Text>();
        StruckText = transform.Find("Panel_Struck/Text").GetComponent<Text>();

        //玩家点击被攻击按钮
        Player1Btn = transform.Find("Player1Btn").GetComponent<Button>();
        Player2Btn = transform.Find("Player2Btn").GetComponent<Button>();
        Player3Btn = transform.Find("Player3Btn").GetComponent<Button>();
        Player4Btn = transform.Find("Player4Btn").GetComponent<Button>();
        Player5Btn = transform.Find("Player5Btn").GetComponent<Button>();

        //添加按钮点击事件
        Player1Btn.onClick.AddListener(OnClickPlayer1Btn);
        Player2Btn.onClick.AddListener(OnClickPlayer2Btn);
        Player3Btn.onClick.AddListener(OnClickPlayer3Btn);
        Player4Btn.onClick.AddListener(OnClickPlayer4Btn);
        Player5Btn.onClick.AddListener(OnClickPlayer5Btn);
    }

    void OnClickPlayer1Btn()
    {
        PlayerAttack(0);
    }

    void OnClickPlayer2Btn()
    {
        PlayerAttack(1);
    }

    void OnClickPlayer3Btn()
    {
        PlayerAttack(2);
    }

    void OnClickPlayer4Btn()
    {
        PlayerAttack(3);
    }

    void OnClickPlayer5Btn()
    {
        PlayerAttack(4);
    }

    //玩家发起攻击
    void PlayerAttack(int index)
    {
        player[index].killnum++;//被攻击玩家死亡数加1
        //Debug.Log("index：" + index);
        //Debug.Log(player[index].name);
        //随机一个玩家击杀数+1(自己除外)
        int i = Random.Range(0, 5);
        //随机出的下标等于自己  重新随机
        while(i==index)
        {
            i = Random.Range(0, 5);
        }
        player[i].strucknum++;//随机出的攻击者击杀数加1
        Score_Sort();
    }

    //榜单排序
    void Score_Sort()
    {
            Player[] Kill_Array = new Player[5];//死亡榜单
            Player[] Struck_Array = new Player[5];//击杀榜单
            for (int l = 0; l < 5;l++ )
            {
                Kill_Array[l] = player[l];
                Struck_Array[l] = player[l];
            }

            //选择排序
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    //选出死亡数最多的玩家
                    if (Kill_Array[i].killnum < Kill_Array[j].killnum)
                    {
                        Player p = Kill_Array[i];
                        Kill_Array[i] = Kill_Array[j];
                        Kill_Array[j] = p;
                    }
                    //选出击杀数最多的玩家
                    if (Struck_Array[i].strucknum < Struck_Array[j].strucknum)
                    {
                        Player p = Struck_Array[i];
                        Struck_Array[i] = Struck_Array[j];
                        Struck_Array[j] = p;
                    }
                }
            }
        string killtext = "";//死亡榜字符拼接
        string strucktext = "";//击杀榜字符拼接
        int ii=1;
        int jj=1;
        //更新死亡榜
        foreach(Player _player in Kill_Array)
        {
            killtext += ii + "：" + _player.name + "  " + _player.killnum + "\n";
            ii++;
        }

        //更新击杀榜
        foreach (Player __player in Struck_Array)
        {
            strucktext += jj + "：" + __player.name + "  " + __player.strucknum + "\n";
            jj++;
        }
        KillText.text = killtext;
        StruckText.text = strucktext;
    }
}
