using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Text;

public class VolumeSet : MonoBehaviour
{
    public TextAsset l;
    private Button SetingBtn;//�������ڽ���
    private Button SetingCloseBtn;//�ر��������ڽ��水ť
    private Button SilenceBtn;//������ť
    private Button Grid_DemoBtn;//��������ť
    private Button On_FileBtn;//�浵��ť
    private Scrollbar scrollbar;
    private GameObject imagePanel;//������������

    private AudioSource audio;
    private bool IsSilence=false;//�Ƿ���

    void Start()
    {
        ReadFile();
    }
    void Awake()
    {
        
        SetingBtn = transform.Find("Button").GetComponent<Button>();
        SetingCloseBtn = transform.Find("Image/CloseBtn").GetComponent<Button>();
        SilenceBtn = transform.Find("Image/Button").GetComponent<Button>();
        Grid_DemoBtn = transform.Find("Grid_Demo").GetComponent<Button>();
        On_FileBtn = transform.Find("On_FileBtn").GetComponent<Button>();
        scrollbar = transform.Find("Image/Scrollbar").GetComponent<Scrollbar>();
        audio = GameObject.Find("AudioSource").GetComponent<AudioSource>();

        //��Ӱ�ť����¼�
        SetingBtn.onClick.AddListener(OnClickSetingBtn);
        SetingCloseBtn.onClick.AddListener(OnClickSetingCloseBtn);
        SilenceBtn.onClick.AddListener(OnClickSilenceBtn);
        Grid_DemoBtn.onClick.AddListener(OnClickGrid_Demo);
        On_FileBtn.onClick.AddListener(OnClickOn_FileBtn);
        

        imagePanel = transform.Find("Image").gameObject;
        imagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //û�о���
        if(!IsSilence)
        {
            audio.volume = scrollbar.value;
        }

    }

    void OnClickSetingBtn()
    {
        imagePanel.SetActive(true);
    }

    void OnClickSetingCloseBtn()
    {
        imagePanel.SetActive(false);
    }

    //���� �������������л�
    void OnClickSilenceBtn()
    {
        Debug.Log("dianji ");
        audio.volume = 0;
        if(!IsSilence)
        {
            IsSilence = true;
        }
        else
        {
            IsSilence = false;
        }
    }

    //���س���
    void OnClickGrid_Demo()
    {
        SceneManager.LoadScene(1);
    }

    //�浵
    void OnClickOn_FileBtn()
    {
        WriteFile();
    }

    //����
    void ReadFile()
    {
        //ͨ��idֵ��ȡ�Ƿ�������Ϣ
        Audioinfo info= AudioInfo.AudioInfoInstance.GetAudioinfoById(1);
        scrollbar.value = info.audioNum;

        if(info.IsSilence=="YES")
        {
            IsSilence = true;
            audio.volume = 0;
        }
    }

    //�浵
    void WriteFile()
    {
        string path = Application.dataPath + "/Resources/TextInfo/AudioInfo.txt";
        string str;
        if(IsSilence)
        {
            str = "1," + scrollbar.value + ",YES";
        }
        else
        {
            str = "1," + scrollbar.value + ",NO";
        }
        File.WriteAllText(path, str);
    }
}
