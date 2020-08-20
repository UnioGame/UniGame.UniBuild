using UnityEngine;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using Interfaces;
    using UnityEditor;

    [Serializable]
    public class ApplyAndroidSettingsCommand : UnitySerializablePreBuildCommand
    {
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.HideLabel]
#endif
        public UniAndroidSettings AndroidSettings = new UniAndroidSettings();
        
        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            EditorUserBuildSettings.androidBuildType = AndroidSettings.AndroidBuildType;
            EditorUserBuildSettings.buildAppBundle = AndroidSettings.BuildAppBundle;
            EditorUserBuildSettings.allowDebugging = AndroidSettings.AllowDebugging;
            EditorUserBuildSettings.exportAsGoogleAndroidProject = AndroidSettings.ExportAsGoogleAndroidProject;
            EditorUserBuildSettings.development = AndroidSettings.IsDevelopment;

            PlayerSettings.Android.targetArchitectures = AndroidSettings.AndroidArchitecture;
            PlayerSettings.Android.forceSDCardPermission = AndroidSettings.ForceSDCardPermission;
            PlayerSettings.Android.forceInternetPermission = AndroidSettings.ForceInternetPermission;
            PlayerSettings.Android.useAPKExpansionFiles = AndroidSettings.UseAPKExpansionFiles;
            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, AndroidSettings.ApiCompatibilityLevel);
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android,AndroidSettings.ScriptingBackend);
        }
    }
}
