using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : BasePanel<GamePanel>
{
    public UIButton btnBack;
    public UILabel labelTime;
    public List<GameObject> hpObjs = new List<GameObject>();
    public float nowTime = 0;
    public override void Init()
    {
        btnBack.onClick.Add(new EventDelegate(() =>
        {
            QuitPanel.Instance.ShowMe();
        }));
        //Invoke("TestFun",5);
    }
    void TestFun()
    {
        GameOverPanel.Instance.ShowMe();
    }
    /// <summary>
    /// 提供给外部改变血量的方法
    /// </summary>
    /// <param name="hp">改变的血量</param>
    public void ChangeHP(int hp)
    {
        for (int i = 0; i < hpObjs.Count; i++)
        {
            hpObjs[i].SetActive(i < hp);
        }
    }
    private void Update()
    {
        nowTime += Time.deltaTime;
        //时间：时分秒
        labelTime.text = "";
        if ((int)nowTime / 3600 > 0)
            labelTime.text += (int)nowTime / 3600 + "h";
        if ((int)nowTime % 3600 / 60 > 0 || labelTime.text != "")
            labelTime.text += (int)nowTime % 3600 / 60 + "m";
        labelTime.text += (int)nowTime % 60 + "s";
    }

}
