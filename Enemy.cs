﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private FSMSystem fsm;

	void Start () {
		InitFSM();
	}

	void InitFSM()
	{
		fsm = new FSMSystem();

		FSMState patrolState = new PatrolState(fsm);
		patrolState.AddTransition(Transition.SeePlayer, StateID.Chase);

		FSMState chaseState = new ChaseState(fsm);
		chaseState.AddTransition(Transition.LostPlayer, StateID.Patrol);

		fsm.AddState(patrolState);
		fsm.AddState(chaseState);
	}

	void Update () {
		//fsm.Update(this.gameObject,);
	}
}
