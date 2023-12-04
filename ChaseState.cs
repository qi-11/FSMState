using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : FSMState
{
	private Transform playerTransform;
	public ChaseState(FSMSystem fsm) : base(fsm)
	{
		stateID = StateID.Chase;
		playerTransform = GameObject.Find("Player").transform;
	}

	public override void Act(GameObject npc,Animator ani)
	{
		npc.transform.LookAt(playerTransform.position);
		npc.transform.Translate(Vector3.forward * 2 * Time.deltaTime);
        ani.SetBool("isrun", true);

    }
    public override void Reason(GameObject npc)
	{
		if (Vector3.Distance(playerTransform.position, npc.transform.position) > 6)
		{
			fsm.PerformTransition(Transition.LostPlayer);
		}

        if (Vector3.Distance(playerTransform.position, npc.transform.position) < 2)
        {
            fsm.PerformTransition(Transition.AtkPlayer);
        }
    }

	
}