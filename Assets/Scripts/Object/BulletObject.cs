using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletObject : MonoBehaviour
{
    //子弹使用的数据
    private BulletInfo info;
    //用于曲线运动的一直增长的计时变量
    private float time;
    //初始化子弹数据的方法
    public void InitInfo(BulletInfo info)
    {
        this.info = info;
        //Destroy(this.gameObject, info.liftTime);
        Invoke("DelayDestroy", info.liftTime);
    }
    private void DelayDestroy()
    {
        //直接执行死亡，会播放特效
        Dead();
    }
    //销毁场景上的子弹
    public void Dead()
    {
        //创建死亡特效
        GameObject effObj = Instantiate(Resources.Load<GameObject>(info.deadEffRes));
        //设置特效的位置，创建在当前子弹的位置
        effObj.transform.position = this.transform.position;
        //1s后特效移除
        Destroy(effObj, 1f);
        //销毁子弹对象
        Destroy(this.gameObject);
    }
    //和对象碰撞（触发）时，子弹消失，飞机减血
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerObject obj = other.GetComponent<PlayerObject>();
            //玩家受伤减血
            obj.Wound();
            //自己销毁并创建死亡特效
            Dead();
        }
    }
    void Update()
    {
        //所有的移动的共同特点，都是朝自己的面朝向移动
        this.transform.Translate(Vector3.forward * info.forwardSpeed * Time.deltaTime);
        //再根据type，进行处理
        //case 1代表只朝向自己面朝向移动
        //2 曲线
        //3 右抛物线
        //4 左抛物线
        //5 跟踪导弹
        switch (info.type)
        {
            case 2:
                time += Time.deltaTime;
                //曲线运动时，旋转速度主要用于控制变化的频率
                this.transform.Translate(Vector3.right * Time.deltaTime * Mathf.Sin(time * info.roundSpeed) * info.rightSpeed);
                break;
            case 3:
                this.transform.rotation *= Quaternion.AngleAxis(info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 4:
                this.transform.rotation *= Quaternion.AngleAxis(-info.roundSpeed * Time.deltaTime, Vector3.up);
                break;
            case 5:
                //跟踪移动，不停的计算玩家和子弹之间的方向向量，然后将自己的角度不停的变化为目标角度 
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(PlayerObject.Instance.transform.position - this.transform.position), info.roundSpeed * Time.deltaTime);
                break;
        }
    }
}
