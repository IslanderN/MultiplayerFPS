using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ComponentsRef : NetworkBehaviour
{

    [SerializeField]
    List<Behaviour> components;

    [SerializeField]
    private ComponentsValues output;

    private void Start()
    {
        AssignRefsForLocalPlayer();
    }

    private void OnDisable()
    {
        // noting here for now
    }

    private void AssignRefsForLocalPlayer()
    {
        if (!isLocalPlayer) return;

        output.Values = new List<Behaviour>();
        foreach (var c in components)
            output.Values.AddRange(components);
    }

}
