using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour
{
    public AudioClip[] audioClip;//存储5个音效
    private AudioSource audio;//音频组件
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        //打开场景时随机一个音效
        index = Random.Range(0, 5);
        audio.clip = audioClip[index];
        audio.Play();
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
