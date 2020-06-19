using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands;
using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces;
using UnityEngine;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using UnityEditor;

    [Serializable]
    public class UniAndroidSettings
    {
        public AndroidBuildType AndroidBuildType = AndroidBuildType.Development;

        public AndroidArchitecture AndroidArchitecture = AndroidArchitecture.ARMv7;

        public ApiCompatibilityLevel ApiCompatibilityLevel = ApiCompatibilityLevel.NET_4_6;
        
        public bool BuildAppBundle = false;

        public bool AllowDebugging = false;
        
        public bool IsDevelopment = false;

        public bool ExportAsGoogleAndroidProject;

        public ScriptingImplementation ScriptingBackend = ScriptingImplementation.Mono2x;
    }
    
    [CreateAssetMenu(
        menuName = "UniGame/UniBuild/Commands/ApplyAndroidSettings", 
        fileName              = nameof(ApplyAndroidSettingsCommand))]
    public class ApplyAndroidSettingsCommand : UnityPreBuildCommand
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

            PlayerSettings.SetApiCompatibilityLevel(BuildTargetGroup.Android, AndroidSettings.ApiCompatibilityLevel);
            PlayerSettings.Android.targetArchitectures = AndroidSettings.AndroidArchitecture;
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android,AndroidSettings.ScriptingBackend);

        }
    }
}
