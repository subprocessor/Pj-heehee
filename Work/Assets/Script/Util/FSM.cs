using UnityEngine;
using System;
using System.Collections.Generic;


public class FSMState
{
	//
	public Type pID { private set; get; }

	//
	protected FSM pFSM { private set; get; }

	//
	protected FSMState(FSM fsm, Type ID)
	{
		pFSM = fsm;
		pID = ID;
	}

	//
	public virtual void Init()
	{
	}

	//
	public virtual void Enter(params object[] parameters)
	{
	}

	//
	public virtual void Exit()
	{
	}
}


public class FSM
{
	//
	protected Dictionary<Type, FSMState> pStates { private set; get; } = new Dictionary<Type, FSMState>();

	//
	public GameObject pGbj { private set; get; }
	public Transform pTrans { private set; get; }

	//
	FSMState _currentState = null;
	FSMState _previousState = null;


	public void SetGbjAndTrans(GameObject gbj, Transform trans)
	{
		pGbj = gbj;
		pTrans = trans;
	}

	public Type GetCurrentStateID()
	{
		//
		if (_currentState == null)
			return null;

		//
		return _currentState.pID;
	}

	public FSMState GetState(Type id)
	{
		if (pStates.ContainsKey(id) == true)
			return pStates[id];

		return null;
	}

	public FSMState GetCurrentState()
	{
		return _currentState;
	}

	public Type GetPreviousStateID()
	{
		return _previousState.pID;
	}

	public FSMState GetPreviousState()
	{
		return _previousState;
	}

	public void AddState(FSMState newState)
	{
		if (newState == null)
		{
			Debug.LogError("FSM ERROR: state null reference is not allowed");
			return;
		}

		if (pStates.ContainsKey(newState.pID) == true)
		{
			Debug.LogError("FSM ERROR: Impossible to add state '" + newState.pID + "' because state has already been added");
			return;
		}

		pStates.Add(newState.pID, newState);
	}

	public void RemoveState(Type Id)
	{
		if (pStates.ContainsKey(Id) == false)
		{
			Debug.LogError("FSM ERROR: Impossible to delete state '" + Id + "'. It was not on the list of stateList");
			return;
		}

		//
		if (pStates[Id] == _currentState)
			pStates[Id].Exit();

		//
		pStates.Remove(Id);
	}

	public void RemoveAllState()
	{
		foreach (var kvp in pStates)
		{
			if (kvp.Value == _currentState)
				kvp.Value.Exit();
		}

		pStates.Clear();

		_currentState = null;
		_previousState = null;
	}

	public void InitStates()
	{
		foreach (var kvp in pStates)
			kvp.Value.Init();
	}

	public void ChangeState(Type id, bool isOverrab = true, params object[] parameters)
	{
		//
		if (pStates.ContainsKey(id) == false)
		{
			Debug.LogError("FSM ERROR: Impossible to add transition state '" + id + "'. It was not on the list of stateList");
			return;
		}

		//
		if (isOverrab == false && _currentState != null && _currentState.pID.Equals(id) == true)
			return;

		//
		if (_currentState != null)
			_currentState.Exit();

		//
		_previousState = _currentState;
		_currentState = pStates[id];

		_currentState.Enter(parameters);
	}

}
