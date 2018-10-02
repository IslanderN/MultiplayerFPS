using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ComponentsValues", menuName = "Variables/ComponentsValues", order = 0)]
public class ComponentsValues : ScriptableObject {
	
	[HideInInspector]
	public  List<Behaviour> Values;

	public void EnableComponents()
	{
		foreach (var value in Values)
		{
			value.enabled = true;
		}
	}

	public void DisableComponents()
	{
		foreach (var value in Values)
		{
			value.enabled = false;
		}
	}

	
}