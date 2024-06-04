using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    void Start()
    {
        //根据开始场景中选择的英雄，动态创建飞机
        RoleInfo info = GameDataMgr.Instance.GetNowSelHeroInfo();
        //根据数据中的资源路径进行动态资源创建
        GameObject obj = Instantiate(Resources.Load<GameObject>(info.resName));
        PlayerObject playerObject = obj.AddComponent<PlayerObject>();
        playerObject.speed = info.speed * 200;
        playerObject.maxHp = 10;
        playerObject.nowHp = info.hp;
        playerObject.roundSpeed = 40;
        //更新界面显示的血量
        GamePanel.Instance.ChangeHP(info.hp);
    }

}
