using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManage : MonoBehaviour
{
    public AudioClip[] audioClip;//�洢5����Ч
    private AudioSource audio;//��Ƶ���
    private int index;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();
        //�򿪳���ʱ���һ����Ч
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
