using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChoosePanel : BasePanel<ChoosePanel>
{
    public UIButton btnClose;
    public UIButton btnLeft;
    public UIButton btnRight;
    public UIButton btnStart;
    //模型父对象
    public Transform heroPos;
    //下方属性相关对象
    public List<GameObject> hpObjs;
    public List<GameObject> speedObjs;
    public List<GameObject> volumnObjs;
    //当前显示的飞机模型对象
    private GameObject airplaneObj;
    public override void Init()
    {
        btnStart.onClick.Add(new EventDelegate(() =>
        {
            SceneManager.LoadScene("GameScene");
        }));
        btnClose.onClick.Add(new EventDelegate(() =>
        {
            HideMe();
            BeginPanel.Instance.ShowMe();
        }));
        btnLeft.onClick.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.nowSelHeroIndex--;
            if (GameDataMgr.Instance.nowSelHeroIndex < 0)
                GameDataMgr.Instance.nowSelHeroIndex = GameDataMgr.Instance.roleData.roleList.Count - 1;
            ChangeNowHero();
        }));
        btnRight.onClick.Add(new EventDelegate(() =>
        {
            GameDataMgr.Instance.nowSelHeroIndex++;
            if (GameDataMgr.Instance.nowSelHeroIndex >= GameDataMgr.Instance.roleData.roleList.Count)
                GameDataMgr.Instance.nowSelHeroIndex = 0;
            ChangeNowHero();
        }));
        HideMe();
    }
    //切换当前的选择
    private void ChangeNowHero()
    {
        RoleInfo info = GameDataMgr.Instance.GetNowSelHeroInfo();
        //更新模型
        //先删除上一次的飞机模型，再创建当前的飞机模型
        DestroyObj();
        airplaneObj = Instantiate(Resources.Load<GameObject>(info.resName));
        //设置父对象
        airplaneObj.transform.SetParent(heroPos, false);
        //设置角度，位置和缩放
        airplaneObj.transform.localPosition = Vector3.zero;
        airplaneObj.transform.localRotation = Quaternion.identity;
        airplaneObj.transform.localScale = Vector3.one * info.scale;
        airplaneObj.layer = LayerMask.NameToLayer("UI");
        //更新属性
        for (int i = 0; i < 10; i++)
        {
            hpObjs[i].SetActive(i < info.hp);
            speedObjs[i].SetActive(i < info.speed);
            volumnObjs[i].SetActive(i < info.volume);
        }
    }
    private void DestroyObj()
    {
        if (airplaneObj != null)
        {
            Destroy(airplaneObj);
            airplaneObj = null;
        }

    }
    //每次默认显示第一个
    public override void ShowMe()
    {
        base.ShowMe();
        GameDataMgr.Instance.nowSelHeroIndex = 0;
        ChangeNowHero();
    }
    //每次面板关闭时删除对象
    public override void HideMe()
    {
        base.HideMe();
        DestroyObj();
    }
    private float time = 0;
    //是否鼠标选中飞机
    private bool isSel = false;
    public Camera UICamera;
    private void Update()
    {
        //让飞机上下浮动
        time += Time.deltaTime;
        heroPos.Translate(Vector3.up * Mathf.Sin(time) * 0.0001f, Space.World);
        //射线检测，让飞机可以转动
        if (Input.GetMouseButtonDown(0))
        {
            //如果点击到了UI层碰撞器，认为需要开始拖动飞机了
            if (Physics.Raycast(UICamera.ScreenPointToRay(Input.mousePosition),1000,1<<LayerMask.NameToLayer("UI"),QueryTriggerInteraction.UseGlobal))
            {
                isSel = true;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isSel = false;
        }
        if (Input.GetMouseButton(0) && isSel)
        {
            heroPos.rotation *= Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 20, Vector3.up);
        }
    }
}
