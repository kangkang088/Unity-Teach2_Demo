using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//开火点位置的类型
public enum E_Pos_Type
{
    Top,
    TopLeft,
    TopRight,
    Right,
    Left,
    Bottom,
    BottomLeft,
    BottomRight,
}
public class FireObject : MonoBehaviour
{
    public E_Pos_Type type;
    //表示屏幕上的点
    private Vector3 screenPos;
    //初始子弹发射方向，作为散弹的初始方向
    private Vector3 initDir;
    //当前开火点的数据信息
    private FireInfo fireInfo;
    private int nowNum;
    private float nowCD;
    private float nowDelay;
    //当前组开火点使用的子弹信息
    private BulletInfo nowBulletInfo;
    //散弹时，每颗子弹的间隔角度
    private float changeAngle;
    //用于发射散弹时，记录上一次发射的方向的
    private Vector3 nowDir;
    void Update()
    {
        //4915
        //print(Camera.main.WorldToScreenPoint(PlayerObject.Instance.transform.position));
        UpdatePos();
        //每一次都检测是否需要重置开火点数据
        ResetFireInfo();
        //发射子弹
        UpdataFire();
    }
    private void UpdatePos()
    {
        screenPos.z = 4915f;
        switch (type)
        {
            case E_Pos_Type.Top:
                screenPos.x = Screen.width / 2;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.TopLeft:
                screenPos.x = 0;
                screenPos.y = Screen.height;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.TopRight:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height;
                initDir = Vector3.left;
                break;
            case E_Pos_Type.Right:
                screenPos.x = Screen.width;
                screenPos.y = Screen.height / 2;
                initDir = Vector3.left;
                break;
            case E_Pos_Type.Left:
                screenPos.x = 0;
                screenPos.y = Screen.height / 2;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.Bottom:
                screenPos.x = Screen.width / 2;
                screenPos.y = 0;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.BottomLeft:
                screenPos.x = 0;
                screenPos.y = 0;
                initDir = Vector3.right;
                break;
            case E_Pos_Type.BottomRight:
                screenPos.x = Screen.width;
                screenPos.y = 0;
                initDir = Vector3.left;
                break;
        }
        //再把屏幕点转成世界坐标点
        this.transform.position = Camera.main.ScreenToWorldPoint(screenPos);
    }
    //重置当前要发射的炮台数据
    public void ResetFireInfo()
    {
        if (nowCD != 0 && nowNum != 0)
        {
            return;
        }
        //组间休息时间判断
        if (fireInfo != null)
        {
            nowDelay -= Time.deltaTime;
            if (nowDelay > 0)
            {
                return;
            }
        }
        //从数据中随机取出一条，按规则发射
        List<FireInfo> list = GameDataMgr.Instance.fireData.fireInfoList;
        fireInfo = list[Random.Range(0, list.Count)];
        //我们不能直接改变数据中的内容，应该用变量临时存储下来
        nowNum = fireInfo.num;
        nowCD = fireInfo.cd;
        nowDelay = fireInfo.delayTime;
        //通过发火点数据，取出当前要使用的子弹信息
        string[] strs = fireInfo.ids.Split(',');
        int beginId = int.Parse(strs[0]);
        int endId = int.Parse(strs[1]);
        int randomBulletId = Random.Range(beginId, endId + 1);
        nowBulletInfo = GameDataMgr.Instance.bulletData.bulletInfoList[randomBulletId - 1];
        if (fireInfo.type == 2)
        {
            switch (type)
            {
                case E_Pos_Type.TopLeft:
                case E_Pos_Type.TopRight:
                case E_Pos_Type.BottomLeft:
                case E_Pos_Type.BottomRight:
                    changeAngle = 90f / (nowNum + 1);
                    break;
                case E_Pos_Type.Top:
                case E_Pos_Type.Right:
                case E_Pos_Type.Left:
                case E_Pos_Type.Bottom:
                    changeAngle = 180f / (nowNum + 1);
                    break;
            }
        }
    }
    //检测开火
    private void UpdataFire()
    {
        //当前不需要发射子弹
        if (nowCD == 0 && nowNum == 0)
        {
            return;
        }
        nowCD -= Time.deltaTime;
        if (nowCD > 0)
        {
            return;
        }
        GameObject bullet;
        BulletObject bulletObj;

        switch (fireInfo.type)
        {
            case 1:
                bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                bulletObj = bullet.AddComponent<BulletObject>();
                bulletObj.InitInfo(nowBulletInfo);
                //设置子弹的位置和朝向
                bullet.transform.position = this.transform.position;
                bullet.transform.rotation = Quaternion.LookRotation(PlayerObject.Instance.transform.position - this.transform.position);
                nowNum--;
                nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                break;
            case 2:
                //无cD
                if (nowCD == 0)
                {
                    for (int i = 0; i < nowNum; i++)
                    {
                        bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                        bulletObj = bullet.AddComponent<BulletObject>();
                        bulletObj.InitInfo(nowBulletInfo);
                        //设置子弹的位置和朝向
                        bullet.transform.position = this.transform.position;
                        //每次都会旋转一个角度
                        nowDir = Quaternion.AngleAxis(changeAngle * i, Vector3.up) * initDir;
                        bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    }
                    nowCD = nowNum = 0;
                }
                else
                {
                    bullet = Instantiate(Resources.Load<GameObject>(nowBulletInfo.resName));
                    bulletObj = bullet.AddComponent<BulletObject>();
                    bulletObj.InitInfo(nowBulletInfo);
                    //设置子弹的位置和朝向
                    bullet.transform.position = this.transform.position;
                    //每次都会旋转一个角度
                    nowDir = Quaternion.AngleAxis(changeAngle * (fireInfo.num - nowNum), Vector3.up) * initDir;
                    bullet.transform.rotation = Quaternion.LookRotation(nowDir);
                    nowNum--;
                    nowCD = nowNum == 0 ? 0 : fireInfo.cd;
                }
                break;
        }
    }
}
