#if ODIN_INSPECTOR

using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UniModules.Editor;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
using UnityEditor;
using UnityEngine;

namespace UniModules.UniGame.UniBuild.Editor.Windows
{

    public class UniBuildConfigurationWindow : OdinEditorWindow
    {
        #region static data

        public static readonly string Title = "Build Configurations";
        
        [MenuItem("UniGame/Uni Build/Show Configs")]
        static void ShowWindow()
        {
            // Get existing open window or if none, make a new one:
            var window = GetWindow<UniBuildConfigurationWindow>();
            window.titleContent = new GUIContent(Title);
            window.InitializeWindow();
            window.Show();
        }
        
        #endregion

        #region public properties
        
        [Searchable]
        [ListDrawerSettings(Expanded = true)]
        [InlineEditor]
        public List<UniBuildCommandsMap> configurations = new List<UniBuildCommandsMap>();

        #endregion
        
        public void InitializeWindow()
        {
            Refresh();
        }

        [Sirenix.OdinInspector.Button]
        public void Refresh()
        {
            configurations.Clear();
            var configs = AssetEditorTools.GetAssets<UniBuildCommandsMap>();
            configurations.AddRange(configs);
        }
        
    }
    
    
}


#endif
