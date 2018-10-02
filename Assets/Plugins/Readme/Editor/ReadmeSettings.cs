using UnityEditor;
using UnityEngine;

namespace Readme
{

    [CreateAssetMenu(fileName = "NewReadmeSettings", menuName = "Readme/ReadmeSettings", order = 0)]
    public class ReadmeSettings : ScriptableObject
    {
        public bool ExitEditModeAutomatically = false;

        [Header("PlaceHodler, not working")]
        public bool LoadLayoutOnStart = false;
        public GameObject Layout;

        private const string assetLabel = "ReadmeSettings";

        private void OnEnable()
        {
            string[] labels = { assetLabel };

            AssetDatabase.SetLabels(this, labels);


        }
    }
}