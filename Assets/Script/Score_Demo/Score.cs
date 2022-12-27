using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    public string name;//�������
    public int killnum;//���������
    public int strucknum;//��һ�ɱ��
}

public class Score : MonoBehaviour
{
    //private List<Player> player = new List<Player>();
    private Player[] player = new Player[5];
    //��ť
    private Button Player1Btn;
    private Button Player2Btn;
    private Button Player3Btn;
    private Button Player4Btn;
    private Button Player5Btn;

    //��ɱ�������ı�
    private Text KillText;
    private Text StruckText;


    void Start()
    {
        Score_Sort();
    }
    // Start is called before the first frame update
    void Awake()
    {
        //ʵ����5����� ����ʼ������
        for(int i=0;i<5;i++)
        {
            player[i] = new Player();
            player[i].name = "���" + (i+1).ToString();
            player[i].killnum = 0;
            player[i].strucknum = 0;
        }

        //�ı�
        KillText = transform.Find("Panel_Kill/Text").GetComponent<Text>();
        StruckText = transform.Find("Panel_Struck/Text").GetComponent<Text>();

        //��ҵ����������ť
        Player1Btn = transform.Find("Player1Btn").GetComponent<Button>();
        Player2Btn = transform.Find("Player2Btn").GetComponent<Button>();
        Player3Btn = transform.Find("Player3Btn").GetComponent<Button>();
        Player4Btn = transform.Find("Player4Btn").GetComponent<Button>();
        Player5Btn = transform.Find("Player5Btn").GetComponent<Button>();

        //��Ӱ�ť����¼�
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

    //��ҷ��𹥻�
    void PlayerAttack(int index)
    {
        player[index].killnum++;//�����������������1
        //Debug.Log("index��" + index);
        //Debug.Log(player[index].name);
        //���һ����һ�ɱ��+1(�Լ�����)
        int i = Random.Range(0, 5);
        //��������±�����Լ�  �������
        while(i==index)
        {
            i = Random.Range(0, 5);
        }
        player[i].strucknum++;//������Ĺ����߻�ɱ����1
        Score_Sort();
    }

    //������
    void Score_Sort()
    {
            Player[] Kill_Array = new Player[5];//������
            Player[] Struck_Array = new Player[5];//��ɱ��
            for (int l = 0; l < 5;l++ )
            {
                Kill_Array[l] = player[l];
                Struck_Array[l] = player[l];
            }

            //ѡ������
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    //ѡ���������������
                    if (Kill_Array[i].killnum < Kill_Array[j].killnum)
                    {
                        Player p = Kill_Array[i];
                        Kill_Array[i] = Kill_Array[j];
                        Kill_Array[j] = p;
                    }
                    //ѡ����ɱ���������
                    if (Struck_Array[i].strucknum < Struck_Array[j].strucknum)
                    {
                        Player p = Struck_Array[i];
                        Struck_Array[i] = Struck_Array[j];
                        Struck_Array[j] = p;
                    }
                }
            }
        string killtext = "";//�������ַ�ƴ��
        string strucktext = "";//��ɱ���ַ�ƴ��
        int ii=1;
        int jj=1;
        //����������
        foreach(Player _player in Kill_Array)
        {
            killtext += ii + "��" + _player.name + "  " + _player.killnum + "\n";
            ii++;
        }

        //���»�ɱ��
        foreach (Player __player in Struck_Array)
        {
            strucktext += jj + "��" + __player.name + "  " + __player.strucknum + "\n";
            jj++;
        }
        KillText.text = killtext;
        StruckText.text = strucktext;
    }
}
