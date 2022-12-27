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
    private Button SetingBtn;//音量调节界面
    private Button SetingCloseBtn;//关闭音量调节界面按钮
    private Button SilenceBtn;//静音按钮
    private Button Grid_DemoBtn;//场景二按钮
    private Button On_FileBtn;//存档按钮
    private Scrollbar scrollbar;
    private GameObject imagePanel;//调节声音界面

    private AudioSource audio;
    private bool IsSilence=false;//是否静音

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

        //添加按钮点击事件
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
        //没有静音
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

    //静音 连续按则来回切换
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

    //加载场景
    void OnClickGrid_Demo()
    {
        SceneManager.LoadScene(1);
    }

    //存档
    void OnClickOn_FileBtn()
    {
        WriteFile();
    }

    //读档
    void ReadFile()
    {
        //通过id值获取是否静音等信息
        Audioinfo info= AudioInfo.AudioInfoInstance.GetAudioinfoById(1);
        scrollbar.value = info.audioNum;

        if(info.IsSilence=="YES")
        {
            IsSilence = true;
            audio.volume = 0;
        }
    }

    //存档
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
