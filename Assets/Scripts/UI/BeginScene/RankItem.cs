using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankItem : MonoBehaviour
{
    public UILabel labelRanking;
    public UILabel labelName;
    public UILabel labelTime;
    /// <summary>
    /// 初始化单条排行榜数据的方法
    /// </summary>
    /// <param name="rank">排名</param>
    /// <param name="name">名字</param>
    /// <param name="time">时间</param>
    public void InitInfo(int rank, string name, int time)
    {
        //排名
        labelRanking.text = rank.ToString();
        //名字
        labelName.text = name;
        //时间：时分秒
        string str = "";
        if (time / 3600 > 0)
            str += time / 3600 + "h";
        if (time % 3600 / 60 > 0 || str != "")
            str += time % 3600 / 60 + "m";
        str += time % 60 + "s";
        labelTime.text = str;
    }
}
