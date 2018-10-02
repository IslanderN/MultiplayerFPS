using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionUI : MonoBehaviour
{

    [HideInInspector]
    public bool On = false;
    private bool oldOnValue = false;
    public void Repaint()
    {
        if (On == oldOnValue) return;
		else
		{
			bool newStatus = On;
			gameObject.SetActive(newStatus);
			oldOnValue = newStatus;
		}

    }
}
