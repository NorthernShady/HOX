using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BasicPriorityStateController<StateType> : MonoBehaviour where StateType : struct, IConvertible, IComparable, IFormattable { 

	public delegate void OnChangeState(StateType oldState, StateType newState);
	public event OnChangeState OnChangeStateEvent;

	StateType m_currentState;

	protected Dictionary<string, StateType> m_requestedState = new Dictionary<string, StateType> ();

	public class RequestState : GameEvent 
	{
		public string objectId;
		public StateType requestedState;
		public bool isForce;
		public RequestState (string objectId, StateType requestedState, bool isForce = false)
		{
			this.objectId = objectId;
			this.requestedState = requestedState;
			this.isForce = isForce;
		}
	}

	void OnEnable ()
	{
		EventManager.Instance.RemoveListener<RequestState> (onRequestState);
		EventManager.Instance.AddListener<RequestState> (onRequestState);
	}

	void OnDisable ()
	{
		EventManager.Instance.RemoveListener<RequestState> (onRequestState);
	}

	protected virtual void onRequestState(RequestState e)
	{
		if (e.isForce) {
			m_requestedState.Clear ();
			setState (e.requestedState);
			return;
		}
		m_requestedState [e.objectId] = e.requestedState;
		List<StateType> requestedStates = m_requestedState.Values.ToList ();
		requestedStates = requestedStates.OrderByDescending (x => getStatePriority (x)).ToList();
		setState (requestedStates.First ());

	}

	protected virtual int getStatePriority (StateType state)
	{
		return (int)(object)state;
	}

	protected virtual void setState (StateType state)
	{
		if (OnChangeStateEvent != null) {
			OnChangeStateEvent.Invoke (m_currentState, state);
			m_currentState = state;
		}
	}
}
