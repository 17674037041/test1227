using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audioinfo
{
    public int id;//id  key值
    public float audioNum;//音量大小
    public string IsSilence;//是否静音
}

public class AudioInfo : MonoBehaviour
{
    public static AudioInfo AudioInfoInstance;//单例
    //音量选项配置表
    private TextAsset AudioAsset;
    //声明字典
    private Dictionary<int, Audioinfo> AudioInfoDic = new Dictionary<int, Audioinfo>();
    void Awake()
    {
        AudioAsset = Resources.Load<TextAsset>("TextInfo/AudioInfo");//加载配置表
        AudioInfoInstance = this;
        ReadInfo();
    }

    void ReadInfo()
    {
        string[] strArray = AudioAsset.text.Split('\n');//得到每一行
        foreach(string str in strArray)//分割每一行
        {
            string[] arrayPro = str.Split(',');
            Audioinfo info = new Audioinfo();
            info.id = int.Parse(arrayPro[0]);
            info.audioNum = float.Parse(arrayPro[1]);
            info.IsSilence = arrayPro[2];
            AudioInfoDic.Add(info.id, info);
        }
    }

    /// <summary>
    /// 提供给外部的接口用于通过id返回对象
    /// </summary>
    /// <returns></returns>
    public Audioinfo GetAudioinfoById(int id)
    {
        Audioinfo info = null;
        AudioInfoDic.TryGetValue(id, out info);
        return info;
    }
}
