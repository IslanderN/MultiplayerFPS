using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

// not abstract because Cmd cant work with abstract parametrs///
public class Logic : ScriptableObject
{

    private bool keepAuthority;

    public virtual void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)
    {
        Debug.Log("Doing Something");
        keepAuthority = false;
    }

    




#if UNITY_EDITOR

    private const string assetLabel = "Logic";
    private void OnEnable()
    {
        List<string> labels = new List<string>();


        labels.Add(assetLabel);

        UnityEditor.AssetDatabase.SetLabels(this, labels.ToArray());
    }
#endif

}
