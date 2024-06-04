using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataMgr
{
    private static GameDataMgr instance = new GameDataMgr();
    public static GameDataMgr Instance => instance;
    //音乐相关数据
    public MusicData musicData;
    //排行榜数据
    public RankData rankData;
    //角色属性数据
    public RoleData roleData;
    //当前选择的角色编号
    public int nowSelHeroIndex = 0;
    //子弹数据
    public BulletData bulletData;
    //开火点数据
    public FireData fireData;
    private GameDataMgr()
    {
        //获取本地磁盘中存储的音乐音效相关数据
        musicData = XmlDataMgr.Instance.LoadData(typeof(MusicData), "MusicData") as MusicData;
        //获取本地磁盘中存储的排行榜相关数据
        rankData = XmlDataMgr.Instance.LoadData(typeof(RankData), "RankData") as RankData;
        //获取本地磁盘中存储的角色属性相关数据
        roleData = XmlDataMgr.Instance.LoadData(typeof(RoleData), "RoleData") as RoleData;
        //获取本地磁盘中存储的子弹数据相关数据
        bulletData = XmlDataMgr.Instance.LoadData(typeof(BulletData), "BulletData") as BulletData;
        //获取本地磁盘中存储的开火点数据相关数据
        fireData = XmlDataMgr.Instance.LoadData(typeof(FireData), "FireData") as FireData;
    }
    #region 音乐音效相关管理的方法
    //保存音乐音效相关数据的方法
    public void SaveMusicData()
    {
        XmlDataMgr.Instance.SaveData(musicData, "MusicData");
    }
    //开关背景音乐的方法
    public void SetMusicIsOpen(bool isOpen)
    {
        //改数据
        musicData.musicIsOpen = isOpen;
        //真正改变开关状态
        BKMusic.Instance.SetBKMusicIsOpen(isOpen);
    }
    //开关音效的方法
    public void SetSoundIsOpen(bool isOpen)
    {
        //改数据
        musicData.soundIsOpen = isOpen;
        //真正改变开关状态
    }
    //设置背景音乐音量大小的方法
    public void SetMusicValue(float value)
    {
        //改数据
        musicData.musicValue = value;
        //真正改slider的状态
        BKMusic.Instance.SetBKMusicValue(value);
    }
    //设置音效音量大小的方法
    public void SetSoundValue(float value)
    {
        //改数据
        musicData.soundValue = value;
    }
    #endregion
    #region 排行榜相关管理的方法
    /// <summary>
    /// 添加排行榜数
    /// </summary>
    /// <param name="name">玩家名</param>
    /// <param name="time">时间</param>
    public void AddRankData(string name, int time)
    {
        //添加并排序，只留20条内容
        //单条数据
        RankInfo rankInfo = new RankInfo();
        rankInfo.name = name;
        rankInfo.time = time;
        rankData.rankList.Add(rankInfo);
        //排序
        rankData.rankList.Sort((a, b) =>
        {
            if (a.time > b.time)
                return -1;
            return 1;
        });
        //移除大于20的
        if (rankData.rankList.Count > 20)
            rankData.rankList.RemoveAt(20);
        //保存数据
        XmlDataMgr.Instance.SaveData(rankData, "RankData");
    }
    #endregion
    #region 玩家数据相关
    /// <summary>
    /// 提供给外部获取当前选择的英雄的数据
    /// </summary>
    /// <returns></returns>
    public RoleInfo GetNowSelHeroInfo()
    {
        return roleData.roleList[nowSelHeroIndex];
    }
    #endregion

}
