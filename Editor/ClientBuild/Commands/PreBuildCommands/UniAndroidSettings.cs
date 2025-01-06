namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using global::UniCore.Runtime.Attributes;
    using Sirenix.OdinInspector;
    using Unity.Android.Types;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Serialization;
    using AndroidArchitecture = UnityEditor.AndroidArchitecture;
    using AndroidBuildType = UnityEditor.AndroidBuildType;

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
        
        public bool overrideTextureCompression = true;
        
        [ShowIf(nameof(overrideTextureCompression))]
        public MobileTextureSubtarget TextureCompression = MobileTextureSubtarget.ASTC;
        [ShowIf(nameof(overrideTextureCompression))]
        public TextureCompressionFormat[] TextureCompressionFormats = new TextureCompressionFormat[]
        {
            TextureCompressionFormat.ASTC
        };
        
        public ScriptingImplementation ScriptingBackend = ScriptingImplementation.Mono2x;
        
#if UNITY_2023_1_OR_NEWER
        
#else 
        public AndroidETC2Fallback ETC2Fallback = AndroidETC2Fallback.Quality32BitDownscaled;
#endif
        
#if UNITY_2021_1_OR_NEWER
        public AndroidCreateSymbols AndroidDebugSymbolsMode = AndroidCreateSymbols.Public;
#endif

#if UNITY_6000_0_OR_NEWER
        [EnumFlags]
        public DebugSymbolFormat DebugSymbolFormat = DebugSymbolFormat.Zip;
#endif

        
    }
}