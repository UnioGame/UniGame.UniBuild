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

        public bool ForceSDCardPermission = false;
        
        public bool ForceInternetPermission = false;

        public bool UseAPKExpansionFiles = false;
        
        public ScriptingImplementation ScriptingBackend = ScriptingImplementation.Mono2x;
    }
}