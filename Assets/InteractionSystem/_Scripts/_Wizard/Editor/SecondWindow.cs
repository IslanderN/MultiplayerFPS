using UnityEngine;
using UnityEditor;
using UnityEditor.Compilation;

public class SecondWindow: ScriptableWizard {

	//[MenuItem("MultyFPS/SecondWindow")]
	public static void MenuEntryCall() {
		DisplayWizard<SecondWindow>("Title");

		//AssemblyReloadEvents.afterAssemblyReload += Something;
	}

	




	private void OnWizardCreate() {
		// if(Application.isPlaying) GUI.enabled = false;

		// Logic logic = ScriptableObject.CreateInstance<Logic>();

		// AssetDatabase.CreateAsset();
	}

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