using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankPanel : BasePanel<RankPanel>
{
    public UIButton btnClose;
    public UIScrollView svList;
    //专门用于存储管理单条数据控件的
    private List<RankItem> itemList = new List<RankItem>();
    public override void Init()
    {
        btnClose.onClick.Add(new EventDelegate(()=>{
            HideMe();
        }));
        HideMe();
        //测试排行榜数据的添加
        //for (int i = 0; i < 8; i++)
        //{
        //    GameDataMgr.Instance.AddRankData("kang" + i, Random.Range(40, 4000));
        //}
    }
    public override void ShowMe()
    {
        base.ShowMe();
        //打开面板时初始化
        //获取本地存储的排行榜数据
        List<RankInfo> list = GameDataMgr.Instance.rankData.rankList;
        for (int i = 0; i < list.Count; i++)
        {
            if (itemList.Count > i)
            {
                itemList[i].InitInfo(i + 1, list[i].name, list[i].time);
            }
            else
            {
                GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RankItem"));
                obj.transform.SetParent(svList.transform, false);
                obj.transform.localPosition = new Vector3(-50, 130 - i * 50, 0);
                //设置数据
                RankItem rankItem = obj.GetComponent<RankItem>();
                rankItem.InitInfo(i + 1, list[i].name, list[i].time);
                //记录
                itemList.Add(rankItem);
            }
        }
    }
}
