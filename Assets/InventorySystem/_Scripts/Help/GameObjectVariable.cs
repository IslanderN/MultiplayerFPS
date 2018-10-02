using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectVariable",menuName = "Variables/GameObjectVariable")]
public class GameObjectVariable : ScriptableObject {
	[HideInInspector]
	public GameObject Value;
	
}
