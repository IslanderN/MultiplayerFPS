using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(InteractionSystem))]
public class InteractionSystemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        InteractionSystem inSys = target as InteractionSystem;

        GUI.enabled = true; // you can press button in Edit and Play mods

        if (GUILayout.Button("Анализ недостающих частей"))
        {
            Something(inSys);
        }
    }

    public void Something(InteractionSystem inSys)
    {

        // variables 
        string mainFolder = InteractionLogicWizard.InteractionSystemFolderName;

        string mainFolderPath = "Assets/" + mainFolder;

        var info = new DirectoryInfo(mainFolderPath);

        var allFolders = info.GetDirectories();

        List<InteractionLinq> linqs = new List<InteractionLinq>();


        // create linqs 
        foreach (var folder in allFolders)
        {
            InteractionLinq linq;
            if (GetInteractionLinq(folder, out linq))
            {
                //Debug.Log("linq.name = " + linq.name);
                //Debug.Log("linq.tag = " + linq.tag);
                //Debug.Log("linq.logic = " + linq.logic);
                linqs.Add(linq);
            }
        }

        // find not listed linqs 

        if (inSys.Dependencies.Count == linqs.Count)
            EditorUtility.DisplayDialog("Глубокий анализ", "все ок", "так и знал");
        else
        {
            if (EditorUtility.DisplayDialog("Глубокий анализ", "Cуществуют не добавленные в систему элементы \nВ системе: " + inSys.Dependencies.Count + "\n" + "Найдено: " + linqs.Count, "запустить поиск несовпадений", "ничего не делать"))
            {
                foreach (var linq in linqs)
                {
                    InteractionLinq equivalent = inSys.Dependencies.Find(l => l.name == linq.name);

                    if (equivalent == null)
                    {
						string newLine = "\n";
						string line1 = "Найден недостающая в системе взаимосвязь";
						string line2 = newLine + "Имя: " + linq.name;
						string line3 = newLine + "Tag: " + linq.tag;
						string line4 = newLine + "Logic: " + linq.logic;
						
						string inforamation = line1 + line2 + line3 + line4;
						

                        if (EditorUtility.DisplayDialog("Глубокий анализ ", inforamation, "добавить", "ничего не делать"))
                        {
                            inSys.Dependencies.Add(linq);
                        }
                    }
                }
            }


        }
    }

    private bool GetInteractionLinq(DirectoryInfo folder, out InteractionLinq lnq)
    {
        InteractionLinq linq = new InteractionLinq();

        Logic logic = ScriptableObject.CreateInstance<Logic>();

        Tag tag = ScriptableObject.CreateInstance<Tag>();

        bool tagFound = false;

        bool logicFound = false;

        var assets = folder.GetFiles("*.asset");

        foreach (var asset in assets)
        {
            if (!logicFound && IsLogic(asset, out logic))
                logicFound = true;

            if (!tagFound && IsTag(asset, out tag))
                tagFound = true;
        }

        if (tagFound && logicFound)
        {
            linq.logic = logic;
            linq.tag = tag;
            linq.name = folder.Name;

            lnq = linq;
            return true;
        }
        else
        {
            lnq = null;
            return false;
        }




    }

    bool IsLogic(FileInfo asset, out Logic lgc)
    {
        var assetPath = asset.FullName.Substring(asset.FullName.IndexOf("Assets"));

        var logic = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Logic)) as Logic;

        if (logic)
        {
            lgc = logic;
            return true;
        }
        else
        {
            lgc = null;
            return false;
        }
    }

    bool IsTag(FileInfo asset, out Tag tg)
    {
        var assetPath = asset.FullName.Substring(asset.FullName.IndexOf("Assets"));

        var tag = AssetDatabase.LoadAssetAtPath(assetPath, typeof(Tag)) as Tag;

        if (tag)
        {
            tg = tag;
            return true;
        }
        else
        {
            tg = null;
            return false;
        }
    }
}