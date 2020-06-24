using UnityEngine;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using Interfaces;
    using UnityEditor;

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
