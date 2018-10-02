using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class NetworkPlayerGizmos : NetworkBehaviour
{

	[SerializeField]
	private float Radius;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
		{
			return;
		}
    }

	private void OnDrawGizmos() {
		if(!isLocalPlayer)
			return;
		Gizmos.DrawWireSphere(transform.position, Radius);
	}

}
