using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tag", menuName = "InteractionSystem/Tag", order = 0)]
public class Tag : ScriptableObject {


    
#if UNITY_EDITOR

    private const string assetLabel = "Tag";
    private void OnEnable()
    {
        List<string> labels = new List<string>();


        labels.Add(assetLabel);

        UnityEditor.AssetDatabase.SetLabels(this, labels.ToArray());
    }
#endif
	
}