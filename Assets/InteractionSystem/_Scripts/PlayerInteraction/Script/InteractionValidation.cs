using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public abstract class InteractionValidation : MonoBehaviour
{
	public virtual bool CanInteract(NetworkBehaviour player)
	{
		
		Debug.Log("Validator base implementation");
		if(locked)
		{
			Debug.Log("base validator, validator is locked, result false");
			return false;
		}
		Debug.Log("base validator validate");
		return true;
	}

    private bool locked = false;

    public bool Locked
    {
        get
        {
            return locked;
        }

        set
        {
            locked = value;
        }
    }

	public float LastValidationAtemptFrame;
	
    // public virtual void BeforeInteraction()
    // {
    // 	// nothing here
    // } 

    // public virtual void AfterInteraction()
    // {
    // 	// noting here
    // }
}