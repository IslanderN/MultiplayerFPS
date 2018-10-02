using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "InteractionSystem", menuName = "InteractionSystem/InteractionSystem/InteractionSystem", order = 0)]
public class InteractionSystem : ScriptableObject {
	public List<InteractionLinq> Dependencies;
}

[System.Serializable]
public class InteractionLinq
{
	public string name;
	public Tag tag;
	public Logic logic;
}