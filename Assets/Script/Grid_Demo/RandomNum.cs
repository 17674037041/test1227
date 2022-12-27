using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RandomNum : MonoBehaviour
{
    private Button Score_DemoBtn;
    private Text[] ChildsText;
    private List<int> NumList1 = new List<int>();
    private List<int> NumList2 = new List<int>();
    private Button RondomNumBtn;
    // Start is called before the first frame update
    void Start()
    {
        Rondom_Num();
    }

    void Awake()
    {
        ChildsText = transform.GetComponentsInChildren<Text>();
        for (int i = 1; i <= 9; i++)
        {
            NumList1.Add(i);
        }

        RondomNumBtn = GameObject.Find("Canvas/RondomBtn").GetComponent<Button>();
        RondomNumBtn.onClick.AddListener(OnClickRondomNumBtn);
        Score_DemoBtn = GameObject.Find("Canvas/Score_DemoBtn").GetComponent<Button>();
        Score_DemoBtn.onClick.AddListener(OnClickScore_DemoBtn);
    }
    void OnClickRondomNumBtn()
    {
        Rondom_Num();
    }
    void OnClickScore_DemoBtn()
    {
        SceneManager.LoadScene(2);
    }
    //�������
    void Rondom_Num()
    {
        foreach(Text text in ChildsText)
        {
            int size = NumList1.Count;
            int index = Random.Range(0, size);//������1�����һ���±�
            text.text = NumList1[index].ToString();
            NumList2.Add(NumList1[index]);//����2�����Ӹ���
            NumList1.Remove(NumList1[index]);//����1�Ƴ��±��Ӧ������
        }
        //����2�е�������������1�� �����´������
        foreach(int num in NumList2)
        {
            NumList1.Add(num);
        }
        //�������2 �����´�������󱣴�����1�е���
        NumList2.Clear();
    }
}
