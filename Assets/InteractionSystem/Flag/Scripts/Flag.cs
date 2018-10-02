using UnityEngine;
using UnityEngine.Networking;

public class Flag : NetworkBehaviour {
    
    // опасно. лучше бы свойство. Но свойство норм не отобразишь и не покажешь. Пока публично для тестов.
    [SyncVar]
    public string FlagOwner = "nobody"; 

    public void ChangeOwner(string newOwner)
    {
        if(!isServer)
            return;
        FlagOwner = newOwner;
    }
}