using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BKMusic : MonoBehaviour
{
    private static BKMusic instance;
    public static BKMusic Instance => instance;
    private AudioSource bkAudio;
    private void Awake()
    {
        instance = this;
        //得到依附在同一个对象上的音效组件
        bkAudio = this.GetComponent<AudioSource>();
        //第一次初始化开关和音量
        SetBKMusicIsOpen(GameDataMgr.Instance.musicData.musicIsOpen);
        SetBKMusicValue(GameDataMgr.Instance.musicData.musicValue);
    }
    private BKMusic()
    {
    }
    //提供给外部开关背景音乐的函数
    public void SetBKMusicIsOpen(bool isOpen)
    {
        bkAudio.mute = !isOpen;
    }
    //提供给外部设置背景音乐音量大小的函数
    public void SetBKMusicValue(float value)
    {
        bkAudio.volume = value;
    }
}
