using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpResult : MonoBehaviour {

	private bool result = false;

	private bool resultNotChecked = false;

    public bool Value
    {
        get
        {
			if(resultNotChecked)
			{
				resultNotChecked = false;
				return result;
			}
            	
			else
			{
				Debug.Log("Player pick up result returned false because no new interactions was made... мда ");
				return false;
			}	
        }
		set
		{
			resultNotChecked = true;
			result = value;
		}
    }
}
