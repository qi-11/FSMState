using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkState : FSMState
{
    private Transform playerTransform;

    private Animator ani;

    public AtkState(FSMSystem fsm) : base(fsm)
    {
        stateID = StateID.Atk;
        //����״̬id
        //Transform pathTransform = GameObject.Find("Path").transform;//����Ŀ���ĸ��ڵ�
        //Transform[] children= pathTransform.GetComponentsInChildren<Transform>();//��ȡ����Ŀ���
        //foreach(Transform child in children)
        //{
        //	if (child != pathTransform)
        //	{
        //		path.Add(child);
        //	}
        //}
        playerTransform = GameObject.Find("Player").transform;
    }



    public override void Act(GameObject npc, Animator ani)
    {
        ani.SetBool("isrun",false);
        ani.SetTrigger("atk");
    }

    public override void Reason(GameObject npc)
    {
        if (Vector3.Distance(playerTransform.position, npc.transform.position) > 2)
        {
            fsm.PerformTransition(Transition.SeePlayer);
        }
    }

}
