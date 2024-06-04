using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 面板基类，所有面板都会继承它，方便面板使用，节约代码量
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BasePanel<T>:MonoBehaviour where T : class
{
    private static T instance;
    public static T Instance => instance;
    protected void Awake()
    {
        instance = this as T;
    }
    private void Start()
    {
        //父类中强制调用 初始化方法
        //该方法是抽象方法，子类又必须去实现
        Init();
    }
    //主要用于初始化控件的事件监听等...
    public abstract void Init();
    public virtual void ShowMe()
    {
        this.gameObject.SetActive(true);
    }
    public virtual void HideMe()
    {
        this.gameObject.SetActive(false);
    }
}
