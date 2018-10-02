using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;



public class PlayerInteractionLogic : ScriptableObject {
	

    public virtual void OnStartLogic(PlayerInteraction player)
    {
        
    }

    public virtual void OnDisableLogic(PlayerInteraction player) {
        
    }

    public virtual void OnPreInteraction(PlayerInteraction player)
    {
        
    }

}