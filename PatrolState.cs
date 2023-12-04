using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatrolState : FSMState
{
	private Vector3 target= Vector3.zero;
	private List<Transform> path = new List<Transform>();//存储巡逻的目标点
	private int index = 0;//目标点下标
	private Transform playerTransform;

	private Animator ani;

	public PatrolState(FSMSystem fsm) : base(fsm)
	{
		stateID = StateID.Patrol;//设置状态id
		//Transform pathTransform = GameObject.Find("Path").transform;//查找目标点的根节点
		//Transform[] children= pathTransform.GetComponentsInChildren<Transform>();//获取所有目标点
		//foreach(Transform child in children)
		//{
		//	if (child != pathTransform)
		//	{
		//		path.Add(child);
		//	}
		//}
		target = new Vector3(UnityEngine.Random.Range(-10,10),0, UnityEngine.Random.Range(-10, 10));
        playerTransform = GameObject.Find("Player").transform;
        

    }

	public override void Act(GameObject npc, Animator ani)
	{
		
        npc.transform.LookAt(target);
        npc.transform.Translate(Vector3.forward * Time.deltaTime * 3);

		ani.SetBool("isrun",true);

        //npc.transform.LookAt(path[index].position);//怪物看向目标点
        //npc.transform.Translate(Vector3.forward * Time.deltaTime * 3);//向目标点移动
        if (Vector3.Distance(npc.transform.position, target) < 1)//如果到达目标点
		{
            target = new Vector3(UnityEngine.Random.Range(-10, 10), 0, UnityEngine.Random.Range(-10, 10));
			//index++;//切换到下一个目标点
			//index %= path.Count;//限制index
		}
	}

	public override void Reason(GameObject npc)
	{
		if (Vector3.Distance(playerTransform.position, npc.transform.position) < 6)
		{
			fsm.PerformTransition(Transition.SeePlayer);//切换到看见玩家的状态
		}
	}
}
