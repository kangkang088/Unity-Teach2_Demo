using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : MonoBehaviour
{
    private static PlayerObject instance;
    public static PlayerObject Instance => instance;
    private void Awake()
    {
        instance = this;
    }
    private PlayerObject()
    {
    }
    //血量
    public int nowHp;
    public int maxHp;
    //速度
    public int speed = 5;
    //旋转速度
    public int roundSpeed;
    //目标角度
    private Quaternion targetQ;
    //死亡状态(默认非死亡)
    public bool isDead = false;
    //记录飞机当前位置(世界坐标转成屏幕坐标的点)
    private Vector3 nowPos;
    //位移前飞机的位置（如果不合法，这个位置将会重新被赋值给飞机）
    private Vector3 frontPos;
    //死亡
    public void Dead()
    {
        isDead = true;
        GameOverPanel.Instance.ShowMe();
    }
    //受伤
    public void Wound()
    {
        if (isDead)
            return;
        //减血
        this.nowHp -= 1;
        //更新面板血量
        GamePanel.Instance.ChangeHP(this.nowHp);
        if (nowHp <= 0)
        {
            this.Dead();
        }
    }
    private float hValue;
    private float vValue;
    void Update()
    {
        if (isDead)
            return;
        //移动，旋转
        hValue = Input.GetAxisRaw("Horizontal");
        vValue = Input.GetAxisRaw("Vertical");
        //没有按AD，目标角度为0
        if (hValue == 0)
            targetQ = Quaternion.identity;
        else
            targetQ = hValue < 0 ? Quaternion.AngleAxis(20, Vector3.forward) : Quaternion.AngleAxis(-20, Vector3.forward);
        //让飞机朝目标角度旋转
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, targetQ, roundSpeed * Time.deltaTime);
        //位移前，记录位置
        frontPos = this.transform.position;
        //移动
        this.transform.Translate(Vector3.forward * vValue * speed * Time.deltaTime);
        this.transform.Translate(Vector3.right * hValue * speed * Time.deltaTime, Space.World);
        //进行极限判断(注意摄像机的渲染目标)
        nowPos = Camera.main.WorldToScreenPoint(this.transform.position);
        //左右
        if (nowPos.x <= 0 || nowPos.x > Screen.width)
            this.transform.position = new Vector3(frontPos.x, this.transform.position.y, this.transform.position.z);
        //上下
        if (nowPos.y <= 0 || nowPos.y > Screen.height)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, frontPos.z);
        //射线检测，用于销毁子弹
        if (Input.GetMouseButtonDown(0))
        {
            //接受射线碰撞到的子弹对象的信息
            RaycastHit hitInfo;
            //这里只检测子弹层
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo,6000, 1 << LayerMask.NameToLayer("Bullet")))
            {
                BulletObject bulletObject = hitInfo.transform.GetComponent<BulletObject>();
                //让被点中的子弹销毁
                bulletObject.Dead();
            }
        }
    }
}
