using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 模型状态
/// </summary>
public enum Transition
{
	NullTransition=0,
	SeePlayer,//看见玩家
	LostPlayer,//丢失玩家
	AtkPlayer//攻击玩家
}

/// <summary>
/// 状态唯一id
/// </summary>
public enum StateID
{
	NullStateID=0,
	Patrol,//巡逻
	Chase,//追击
	Atk//攻击
}

/// <summary>
/// 状态机基类
/// </summary>
public abstract class FSMState{

	protected StateID stateID;
	public StateID ID { get { return stateID; } }

    /// <summary>
    /// //存储模型所有可以转换的状态
    /// </summary>
    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();

	protected FSMSystem fsm;//状态机系统

	public FSMState(FSMSystem fsm)
	{
		this.fsm = fsm;
	}

	/// <summary>
	/// 添加状态
	/// </summary>
	/// <param name="trans"></param>
	/// <param name="id"></param>
	public void AddTransition(Transition trans,StateID id)
	{
		if (trans == Transition.NullTransition)
		{
			Debug.LogError("不允许NullTransition");return;
		}
		if (id == StateID.NullStateID)
		{
			Debug.LogError("不允许NullStateID"); return;
		}
		if (map.ContainsKey(trans))
		{
			Debug.LogError("添加转换条件的时候，" + trans + "已经存在于map中");return;
		}
		map.Add(trans, id);
	}

	/// <summary>
	/// 删除状态
	/// </summary>
	/// <param name="trans"></param>
	public void DeleteTransition(Transition trans)
	{
		if (trans == Transition.NullTransition)
		{
			Debug.LogError("不允许NullTransition"); return;
		}
		if (map.ContainsKey(trans)==false)
		{
			Debug.LogError("删除转换条件的时候，" + trans + "不存在于map中"); return;
		}
		map.Remove(trans);
	}

	/// <summary>
	/// 获取状态id
	/// </summary>
	/// <param name="trans"></param>
	/// <returns></returns>
	public StateID GetOutputState(Transition trans)
	{
		if (map.ContainsKey(trans))
		{
			return map[trans];
		}
		return StateID.NullStateID;
	}

	/// <summary>
	/// 进入状态前
	/// </summary>
	public virtual void DoBeforeEntering() { }

	/// <summary>
	/// 离开状态后
	/// </summary>
	public virtual void DoAfterLeaving() { }

	/// <summary>
	/// 状态执行时的逻辑
	/// </summary>
	/// <param name="npc"></param>
	public abstract void Act(GameObject npc,Animator ani);

	/// <summary>
	/// 状态执行的条件
	/// </summary>
	/// <param name="npc"></param>
	public abstract void Reason(GameObject npc);
}
