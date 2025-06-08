#if ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UniGame.UniBuild.Editor;
using UnityEditor;
using UnityEngine;

namespace UniModules.UniGame.UniBuild.Editor.Windows
{

    public class UniBuildConfigurationWindow : OdinEditorWindow
    {
        #region static data

        public static readonly string Title = "Build Configurations";
        
        [MenuItem("UniGame/Uni Build/Show Configs")]
        public static void ShowWindow()
        {
            // Get existing open window or if none, make a new one:
            var window = GetWindow<UniBuildConfigurationWindow>();
            window.titleContent = new GUIContent(Title);
            window.InitializeWindow();
            window.Show();
        }
        
        #endregion

        #region public properties

        [HideLabel]
        [InlineEditor(InlineEditorObjectFieldModes.Boxed)]
        public UniBuildConfigurationAsset configurationAsset;
        
        #endregion

        public void InitializeWindow()
        {
            configurationAsset = CreateInstance<UniBuildConfigurationAsset>();
            configurationAsset.Refresh();
        }

    }
    
    
}


#endif
