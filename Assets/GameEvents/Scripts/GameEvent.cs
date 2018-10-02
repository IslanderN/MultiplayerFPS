using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Events/GameEvent", order = 0)]
public class GameEvent : ScriptableObject {
	private List<GameEventListener> listerens = new List<GameEventListener>();

	public void Raise()
	{
		for(int i = listerens.Count - 1; i >= 0; i--)
			listerens[i].OnEventRaised();
	}

	public void RegisterListener(GameEventListener listener)
	{ listerens.Add(listener);}

	public void UnRegisterListener(GameEventListener listener)
	{ listerens.Remove(listener);}

	
}