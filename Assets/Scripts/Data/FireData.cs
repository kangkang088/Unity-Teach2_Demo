using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 开火点数据集合
/// </summary>
public class FireData 
{
    public List<FireInfo> fireInfoList = new List<FireInfo>();
}
/// <summary>
/// 单个开火点的数据信息
/// </summary>
public class FireInfo
{
    [XmlAttribute]
    public int id;//开火点id，方便配置
    [XmlAttribute]
    public int type;//开火点开火类型，1-顺序，2-散弹
    [XmlAttribute]
    public int num;//开火点一组子弹发射的数量
    [XmlAttribute]
    public float cd;//每组子弹中每个子弹的间隔时间
    [XmlAttribute]
    public string ids;//关联的子弹id  1,10代表id 1-10 中的子弹中去随机
    [XmlAttribute]
    public float delayTime;//组间间隔时间
}
