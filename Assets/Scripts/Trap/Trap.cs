using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    #region 属性
    public float damage;//伤害
    public float activeTime;//激活时间
    protected string _name;//名字
    public Vector3 dir;//方向
    public string Name
    {
        set
        {
            _name = value;
            gameObject.name = _name;
        }
        get
        {
            return name;
        }
    }
    public int ID //陷阱id
    {
        protected set;
        get;
    }

    #endregion

    #region MonoBehaviour自身方法
    protected void Update()
    {
        UpdateEntity(Time.deltaTime, dir);
    }

    protected void OnDestroy()
    {
        DestroyEntity();
    }
    #endregion

    #region 虚方法 ---初始化、更新、启用、禁用、销毁
    public virtual void Init(int id)//初始化
    {
        ID = id;//默认名字为id
        Name = id.ToString();
    }
    public virtual void EnableEntity(Vector3 pos)//启用陷阱
    {
        gameObject.transform.position = pos;
    }
    protected virtual void UpdateEntity(float detaTime, Vector3 dir)//更新陷阱
    {

    }
    public virtual void DisableEntity()//禁用陷阱
    {

    }
    public virtual void DestroyEntity()//摧毁陷阱
    {

    }
    #endregion

    #region Cmd 命令
    public delegate void OnReceiveCmd(ICmd data);//接收命令的委托

    //命令列表
    protected Dictionary<EnumCmdType, OnReceiveCmd> dicCmds = new Dictionary<EnumCmdType, OnReceiveCmd>();


    //添加命令
    protected void AddCmd(EnumCmdType cmd, OnReceiveCmd func)
    {
        if (dicCmds.ContainsKey(cmd))
        {
            Debug.LogError("Error: " + _name + " 重复添加 " + cmd + " 命令");
            return;
        }
        dicCmds.Add(cmd, func);
    }

    //移除命令
    protected void RemoveCmd(EnumCmdType cmd)
    {
        if (!dicCmds.ContainsKey(cmd))
        {
            Debug.LogError("Error: " + _name + " 对象不存在此命令:" + cmd);
            return;
        }
        dicCmds.Remove(cmd);
    }

    //接收命令
    public void ReceiveCmd(EnumCmdType cmd, ICmd data)
    {
        if (!dicCmds.ContainsKey(cmd))
        {
            Debug.LogError("Error: " + _name + " 对象不存在此命令:" + cmd + " 触发命令失败");
            return;
        }
        dicCmds[cmd].Invoke(data);
    }

    #endregion

    public interface ICmd//命令接口
    {
        void AddCmd(EnumCmdType cmd, OnReceiveCmd func);//添加命令
        void RemoveCmd(EnumCmdType cmd);//移除命令
        void ReceiveCmd(EnumCmdType cmd, ICmd data);//接收命令
    }
    public enum EnumCmdType//陷阱命令
    {
        None,
    }

}