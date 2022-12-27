using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audioinfo
{
    public int id;//id  keyֵ
    public float audioNum;//������С
    public string IsSilence;//�Ƿ���
}

public class AudioInfo : MonoBehaviour
{
    public static AudioInfo AudioInfoInstance;//����
    //����ѡ�����ñ�
    private TextAsset AudioAsset;
    //�����ֵ�
    private Dictionary<int, Audioinfo> AudioInfoDic = new Dictionary<int, Audioinfo>();
    void Awake()
    {
        AudioAsset = Resources.Load<TextAsset>("TextInfo/AudioInfo");//�������ñ�
        AudioInfoInstance = this;
        ReadInfo();
    }

    void ReadInfo()
    {
        string[] strArray = AudioAsset.text.Split('\n');//�õ�ÿһ��
        foreach(string str in strArray)//�ָ�ÿһ��
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
    /// �ṩ���ⲿ�Ľӿ�����ͨ��id���ض���
    /// </summary>
    /// <returns></returns>
    public Audioinfo GetAudioinfoById(int id)
    {
        Audioinfo info = null;
        AudioInfoDic.TryGetValue(id, out info);
        return info;
    }
}
