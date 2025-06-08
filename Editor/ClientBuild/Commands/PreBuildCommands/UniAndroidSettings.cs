namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands
{
    using System;
    using global::UniCore.Runtime.Attributes;
    using Sirenix.OdinInspector;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Serialization;
    using AndroidArchitecture = UnityEditor.AndroidArchitecture;
    using AndroidBuildType = UnityEditor.AndroidBuildType;

#if UNITY_6000_0_OR_NEWER && UNITY_ANDROID
    using Unity.Android.Types;
#endif
    
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

        [EnumFlags]
        public DebugSymbolFormat DebugSymbolFormat = DebugSymbolFormat.Zip;
        
    }
    
    #if !UNITY_ANDROID || !UNITY_6000_0_OR_NEWER
    /// <summary>
    /// dummy enum for unity bellow UNITY_6000_0_OR_NEWER
    /// </summary>
    public enum DebugSymbolFormat
    {
        Zip,//	Includes debug metadata into the zip file.
        IncludeInBundle,//	Embeds the debug metadata into the app bundle.This option doesn't apply if you're producing an apk or a gradle project.
        LegacyExtensions,//	Produces debug metadata files with .so extension in the zip package.
    }
    #endif
}