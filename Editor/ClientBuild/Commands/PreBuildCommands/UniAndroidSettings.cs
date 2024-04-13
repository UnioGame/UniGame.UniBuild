namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Serialization;

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

        /// <summary>
        /// UseAPKExpansionFiles is deprecated in Unity 2023.1
        /// </summary>
        [Tooltip("equivalent to PlayerSettings.Android.UseAPKExpansionFiles for Unity before 2023.1")]
        public bool SplitApplicationBinary = false;
        public bool AutoConnetcProfiler = false;
        
        public Il2CppCompilerConfiguration CppCompilerConfiguration = Il2CppCompilerConfiguration.Debug;
        
        public MobileTextureSubtarget TextureCompression = MobileTextureSubtarget.ASTC;
        
        public ScriptingImplementation ScriptingBackend = ScriptingImplementation.Mono2x;
        
#if UNITY_2023_1_OR_NEWER
        public AndroidCreateSymbols AndroidDebugSymbolsMode = AndroidCreateSymbols.Public;
#elif UNITY_2021_1_OR_NEWER
        public AndroidETC2Fallback ETC2Fallback = AndroidETC2Fallback.Quality32BitDownscaled;
#else
        
#endif

        
    }
}