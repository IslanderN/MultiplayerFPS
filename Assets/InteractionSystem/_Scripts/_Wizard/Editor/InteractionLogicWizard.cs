using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.Callbacks;
// using static TimelinePlayableWizard;

public class InteractionLogicWizard : ScriptableWizard
{

    const string k_Tab = "    ";

    const string suffix_1 = "Logic";
    const string suffix_2 = "Behaviour";

    public static string InteractionSystemFolderName = "InteractionSystem";

    public string fileName = "SomeObject";



    [MenuItem("InteractionSystem/Interactable Logic Wizzard")]
    private static void MenuEntryCall()
    {
        DisplayWizard<InteractionLogicWizard>("Interactable Logic Wizzard");
    }

    void OnWizardUpdate()
    {
        helpString = 
        "1. Выбери название обекта, например Трон. " + "\n" +
        "2. Нажми Create" + "\n" +
        "3. Создай в созданой папке нужный файлы для логики (Create/Assets/InteractionSystem/Logic + {имя обекта}" + suffix_1 + ")" + "\n" +
        "4. Создай в созданой папке Tag (Create/Assets/InteractionSystem/Tag) и назови его  {имя обекта}Tag " + "\n" +
        "5. Выбери файл InteractionSystem (в папке InteractionSystem) и нажми кнопку, взаимосвязи между логикой и связью добавяться автоматически" + "\n" +
        "6. Создай GameObject и добавь в него компонент Interactable. Там добавь созданый Tag." + "\n" +
        "7. В созданый GameObject добавь компонент {имя обекта}" + suffix_2 + "." + "\n" + 
        "8. Програмируй и тестируй. Обратись к комуто за помощью, посмотри говтоый код, не забывай про TargetRpc и ClientRpc" + "\n" +
        "9. Попроси меня добавить коментарии в код :)" + "\n"; 
    }

    private void OnWizardCreate()
    {
        // creating folders 
        AssetDatabase.CreateFolder("Assets/" + InteractionSystemFolderName, fileName);

        AssetDatabase.CreateFolder("Assets/" + InteractionSystemFolderName + "/" + fileName, "Scripts");

        // creating script_1
        string path = Application.dataPath + "/" + InteractionSystemFolderName + "/" + fileName + "/" + "Scripts" + "/" + fileName + suffix_1 + ".cs";
        using (StreamWriter writer = File.CreateText(path))
        {
            writer.Write(LogicScript());
        }


        // creating script_2
        path = Application.dataPath + "/" + InteractionSystemFolderName + "/" + fileName + "/" + "Scripts" + "/" + fileName + suffix_2 + ".cs";
        using (StreamWriter writer = File.CreateText(path))
        {
            writer.Write(BehaviourScript());
        }

        // Display dialoge 
        EditorUtility.DisplayDialog("Внимание", "Осталось подождать пока все скомпилируется", "Жду", "Блин...");


        // refresh database
        AssetDatabase.Refresh();

        // selecting folder in Project browser
            // Load folder
        path = Application.dataPath + "/" + InteractionSystemFolderName + "/" + fileName;
        UnityEngine.Object createdFolder = AssetDatabase.LoadAssetAtPath(path, typeof(UnityEngine.Object));

            // Select this folder
        Selection.activeObject = createdFolder;

            // Also flash the folder yellow to highlight it
        EditorGUIUtility.PingObject(createdFolder);

        // open second window
        // SecondWindow.MenuEntryCall();

    }

    #region Sample


    // string PlayableAsset ()
    // {
    //     return 
    //         "using System;\n" +
    //         "using UnityEngine;\n" +
    //         "using UnityEngine.Playables;\n" +
    //         "using UnityEngine.Timeline;\n" +
    //         AdditionalNamespacesToString() +
    //         "\n" +
    //         "[Serializable]\n" +
    //         "public class " + playableName + k_TimelineClipAssetSuffix + " : PlayableAsset, ITimelineClipAsset\n" +
    //         "{\n" +
    //         k_Tab + "public " + playableName + k_TimelineClipBehaviourSuffix + " template = new " + playableName + k_TimelineClipBehaviourSuffix + " ();\n" +
    //         ExposedReferencesToString () +
    //         "\n" +
    //         k_Tab + "public ClipCaps clipCaps\n" +
    //         k_Tab + "{\n" +
    //         k_Tab + k_Tab + "get { return " + ClipCapsToString () + "; }\n" +
    //         k_Tab + "}\n" +
    //         "\n" +
    //         k_Tab + "public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)\n" +
    //         k_Tab + "{\n" +
    //         k_Tab + k_Tab + "var playable = ScriptPlayable<" + playableName + k_TimelineClipBehaviourSuffix + ">.Create (graph, template);\n" +
    //         ExposedReferencesResolvingToString () +
    //         k_Tab + k_Tab + "return playable;\n" +
    //         k_Tab + "}\n" +
    //         "}\n";
    // }

    #endregion

    string LogicScript()
    {
        return
            "using System.Collections;\n" +
            "using System.Collections.Generic;\n" +
            "using UnityEngine;\n" +
            "using UnityEngine.Networking;\n" +
            // AdditionalNamespacesToString();
            "\n" +

            "[CreateAssetMenu(fileName =  \"" + fileName + suffix_1 + "\"" + ", menuName = " + "\"" + InteractionSystemFolderName + "/" + suffix_1 + "/" + fileName + suffix_1 + "\"" + ")" + "]" + "\n" +
            "public class " + fileName + suffix_1 + ": Logic \n" +
            "{" + "\n" +
            k_Tab + "public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)" + "\n" +
            k_Tab + "{" + "\n" +
            k_Tab + k_Tab + "Debug.Log(\"Doing Something\");" + "\n" +
            k_Tab + k_Tab + "RenameThisFunction(interactableObj, player, out keepAuthority);" + "\n" +
            k_Tab + "}" + "\n" +
            k_Tab + "\n " +
            k_Tab + "private void RenameThisFunction (NetworkIdentity interactableObj, NetworkIdentity player, out bool keepAuthority)" + "\n" +
            k_Tab + "{" + "\n" +
            k_Tab + k_Tab + "keepAuthority = false; // work in progress, just keep it false" + "\n" +
            k_Tab + k_Tab + "var renameMe = interactableObj.GetComponent<" + fileName + suffix_2 + ">();" + "\n" +
            k_Tab + "}" + "\n" +
            "}" + "\n";
    }

    string BehaviourScript()
    {
        return

            "using System.Collections;" + "\n" +
            "using System.Collections.Generic;" + "\n" +
            "using UnityEngine;" + "\n" +
            "using UnityEngine.Networking;" + "\n" +
            "\n" +

            "public class " + fileName + suffix_2 + " : NetworkBehaviour" + "\n" +
            "{" + "\n" +
            k_Tab + "// methods from this class will be used by " + fileName + suffix_1 + " class" + "\n" + 
            k_Tab + "// use RpcClinet and TargetRpc atributes as in examples " + "\n" + 
            "}" + "\n";
    }


    #region Something else


    // }

    // string AdditionalNamespacesToString ()
    // {
    //     UsableType[] exposedReferenceTypes = Variable.GetUsableTypesFromVariableArray (exposedReferences.ToArray ());
    //     UsableType[] behaviourVariableTypes = Variable.GetUsableTypesFromVariableArray (playableBehaviourVariables.ToArray ());
    //     UsableType[] allUsedTypes = new UsableType[exposedReferenceTypes.Length + behaviourVariableTypes.Length + 1];
    //     for (int i = 0; i < exposedReferenceTypes.Length; i++)
    //     {
    //         allUsedTypes[i] = exposedReferenceTypes[i];
    //     }
    //     for (int i = 0; i < behaviourVariableTypes.Length; i++)
    //     {
    //         allUsedTypes[i + exposedReferenceTypes.Length] = behaviourVariableTypes[i];
    //     }
    //     allUsedTypes[allUsedTypes.Length - 1] = trackBinding;

    //     string[] distinctNamespaces = UsableType.GetDistinctAdditionalNamespaces (allUsedTypes).Where (x => !string.IsNullOrEmpty (x)).ToArray ();
    //     string returnVal = "";
    //     for (int i = 0; i < distinctNamespaces.Length; i++)
    //     {
    //         returnVal += "using " + distinctNamespaces[i] + ";\n";
    //     }
    //     return returnVal;
    // }


    #endregion

    // void OnEnable()
    // {
    //     AssemblyReloadEvents.beforeAssemblyReload += OnBeforeAssemblyReload;
    //     AssemblyReloadEvents.afterAssemblyReload += OnAfterAssemblyReload;
    // }

    // void OnDisable()
    // {
    //     AssemblyReloadEvents.beforeAssemblyReload -= OnBeforeAssemblyReload;
    //     AssemblyReloadEvents.afterAssemblyReload -= OnAfterAssemblyReload;
    // }

    // public void OnBeforeAssemblyReload()
    // {
    //     Debug.Log("Before Assembly Reload");
    // }

    // public void OnAfterAssemblyReload()
    // {
    //     Debug.Log("After Assembly Reload");
    // }
}

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Networking;

// [CreateAssetMenu(fileName = "ChairLogic", menuName = "InteractionSystem/Logic/ChairLogic", order = 0)]
// public class ChairLogic : Logic
// {
//     public override void DoSomething(NetworkIdentity interactableObj, NetworkIdentity player)
//     {
//         Debug.Log("Doing Something");
//         UseChair(interactableObj, player);
//     }

//     private void UseChair(NetworkIdentity interactableObj, NetworkIdentity player)
//     {
//         Debug.Log("Using chair");
//         var chair = interactableObj.GetComponent<ChairBehaviour>();
//         // var playerPosition = player.transform.position;

//         // playerPosition.CmdMovePlayerToPosition(chair.SittingPlace.position);

//         chair.RpcMovePlayerOnSittingPosition(player);
//     }
// }
