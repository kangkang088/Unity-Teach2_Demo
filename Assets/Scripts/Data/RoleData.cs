using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

/// <summary>
/// 所有角色的数据集合
/// </summary>
public class RoleData 
{
    public List<RoleInfo> roleList = new List<RoleInfo>();
}
/// <summary>
/// 单个角色的数据
/// </summary>
public class RoleInfo
{
    [XmlAttribute]
    public int hp;
    [XmlAttribute]
    public int speed;
    [XmlAttribute]
    public int volume;
    [XmlAttribute]
    public string resName;//资源路径
    [XmlAttribute]
    public float scale;//选角面板使用的模型的缩放大小
}
