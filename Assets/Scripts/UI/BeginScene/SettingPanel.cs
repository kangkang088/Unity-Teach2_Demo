using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel<SettingPanel>
{
    public UIButton btnClose;
    public UISlider sliderMusic;
    public UISlider sliderSound;
    public UIToggle toggleMusic;
    public UIToggle toggleSound;
    public override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(() => { HideMe(); }));
        sliderMusic.onChange.Add(new EventDelegate(() =>
        {
            //改变音量大小 还要改变数据
            GameDataMgr.Instance.SetMusicValue(sliderMusic.value);
        }));
        sliderSound.onChange.Add(new EventDelegate(() =>
        {
            //改变音效大小 还要改变数据
            GameDataMgr.Instance.SetSoundValue(sliderSound.value);
        }));
        toggleMusic.onChange.Add(new EventDelegate(() =>
        {
            //背景音乐开关
            GameDataMgr.Instance.SetMusicIsOpen(toggleMusic.value);
        }));
        toggleSound.onChange.Add(new EventDelegate(() =>
        {
            //背景音效开关
            GameDataMgr.Instance.SetSoundIsOpen(toggleSound.value);
        }));
        HideMe();
    }
    //显示的时候初始化的是上一次选择的数据
    public override void ShowMe()
    {
        base.ShowMe();
        MusicData musicData = GameDataMgr.Instance.musicData;
        toggleMusic.value = musicData.musicIsOpen;
        toggleSound.value = musicData.soundIsOpen;
        sliderMusic.value = musicData.musicValue;
        sliderSound.value = musicData.soundValue;

    }
    //隐藏自己的时候，保存这次选择的数据
    public override void HideMe()
    {
        base.HideMe();
        GameDataMgr.Instance.SaveMusicData();
    }
}
