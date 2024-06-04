using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 子弹数据集合
/// </summary>
public class BulletData 
{
    public List<BulletInfo> bulletInfoList = new List<BulletInfo>();
}
/// <summary>
/// 单条子弹数据信息
/// </summary>
public class BulletInfo
{
    [XmlAttribute]
    public int id;//子弹数据的ID
    [XmlAttribute]
    public int type;//子弹移动规则,1-5代表不同的五种
    [XmlAttribute]
    public float forwardSpeed;//正朝向移动速度
    [XmlAttribute]
    public float rightSpeed;//横向移动速度
    [XmlAttribute]
    public float roundSpeed;//追踪子弹的旋转速度
    [XmlAttribute]
    public string resName;//子弹资源路径
    [XmlAttribute]
    public string deadEffRes;//子弹销毁资源路径
    [XmlAttribute]
    public float liftTime;//子弹存在时间
}
