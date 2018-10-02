using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LocalPlayerRef : NetworkBehaviour
{
	[SerializeField]
	private GameObjectVariable LocalPlayer;

	private void SetRef()
	{
		if(isLocalPlayer)
			LocalPlayer.Value = this.gameObject;
	}
    private void Start()
    {
		SetRef();
    }

	

    private void OnEnable()
    {
		SetRef();
    }

    private void OnDisable()
    {
		SetRef();
    }


}
