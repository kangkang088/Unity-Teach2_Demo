using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : BasePanel<GameOverPanel>
{
    public UILabel labelTime;
    public UIInput inputName;
    public UIButton btnSure;
    private int endTime;
    public override void Init()
    {
        btnSure.onClick.Add(new EventDelegate(() =>
        {
            //保存数据
            GameDataMgr.Instance.AddRankData(inputName.value, endTime);
            //结束游戏
            SceneManager.LoadScene("BeginScene");
        }));
        HideMe();
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //显示该面板时，就应该记录当前的时间
        endTime = (int)GamePanel.Instance.nowTime;
        //游戏界面得到当前的时间
        labelTime.text = GamePanel.Instance.labelTime.text;
    }
}
