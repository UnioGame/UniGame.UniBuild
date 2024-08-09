namespace UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using UniModules.UniGame.UniBuild;
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEngine;
    using UnityEngine.Serialization;

#if TRI_INSPECTOR
    using TriInspector;
#endif

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

    [Serializable]
    public class UniBuildConfigurationData
    {
        [Tooltip("use application name as artifact name")]
        public bool overrideArtifactName = true;

        [Tooltip("artifact name")]
        [ShowIf(nameof(overrideArtifactName))]
        public string artifactName = string.Empty;

        [Tooltip("override product name")]
        public bool overrideProductName = false;

        [Tooltip("artifact name")]
        [ShowIf(nameof(overrideProductName))]
        public string productName = string.Empty;

        [Tooltip("override bundle name")]
        public bool overrideBundleName = false;

        [ShowIf(nameof(overrideBundleName))]
        [Tooltip("use application name as bundle name")]
        public string bundleName = string.Empty;

        [Tooltip("override bundle name")]
        public bool overrideCompanyName = false;

        [ShowIf(nameof(overrideCompanyName))]
        [Tooltip("game company name")]
        public string companyName = string.Empty;

        [FormerlySerializedAs("_buildTarget")]
        [SerializeField]
        public BuildTarget buildTarget;

        [FormerlySerializedAs("_buildTargetGroup")]
        [SerializeField]
        public BuildTargetGroup buildTargetGroup;

#if ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf(nameof(IsShownStandaloneSubTarget))]
#endif
        public StandaloneBuildSubtarget standaloneBuildSubTarget = StandaloneBuildSubtarget.Player;
        
        public ScriptingImplementation scriptingImplementation = ScriptingImplementation.Mono2x;
        public Il2CppCodeGeneration il2CppCodeGeneration = Il2CppCodeGeneration.OptimizeSpeed;
        public Il2CppCompilerConfiguration cppCompilerConfiguration = Il2CppCompilerConfiguration.Release;
        
#if ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf(nameof(IsWebGL))]
        [InlineProperty]
        [HideLabel]
        [FoldoutGroup("WebGL")]
#endif
        public WebGlBuildData webGlBuildData = new();
        
        public bool IsWebGL => buildTarget == BuildTarget.WebGL;

#if ODIN_INSPECTOR || TRI_INSPECTOR
        [BoxGroup(nameof(buildOptions))]
#endif
        [Tooltip("development build")]
        public bool developmentBuild;

#if ODIN_INSPECTOR || TRI_INSPECTOR
        [BoxGroup(nameof(buildOptions))]
        [ShowIf(nameof(developmentBuild))]
#endif
        public bool autoconnectProfiler;

#if ODIN_INSPECTOR || TRI_INSPECTOR
        [BoxGroup(nameof(buildOptions))]
        [ShowIf(nameof(developmentBuild))]
#endif
        [Tooltip("enable deep profiling")]
        public bool deepProfiling;

#if ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf(nameof(developmentBuild))]
        [BoxGroup(nameof(buildOptions))]
#endif
        [Tooltip("allow script debugging")]
        public bool scriptDebugging;
        
        [BoxGroup(nameof(buildOptions))]
        public bool runOnBuildFinish;
        
        [BoxGroup(nameof(buildOptions))]
        public BuildOptions buildOptions = BuildOptions.None;

#if ODIN_INSPECTOR
        [FoldoutGroup("Logging")]
#endif
        public bool overrideLogsSettings = false;
        
#if ODIN_INSPECTOR
        [FoldoutGroup("Logging")]
        [ShowIf(nameof(overrideLogsSettings))]
#endif
        public StackTraceLogType logsLevel = StackTraceLogType.None;
        
#if ODIN_INSPECTOR
        [FoldoutGroup("Logging")]
        [ShowIf(nameof(overrideLogsSettings))]
#endif
        public StackTraceLogType warningLevel = StackTraceLogType.None;
        
#if ODIN_INSPECTOR
        [FoldoutGroup("Logging")]
        [ShowIf(nameof(overrideLogsSettings))]
#endif
        public StackTraceLogType errorLevel = StackTraceLogType.ScriptOnly;
        
#if ODIN_INSPECTOR
        [FoldoutGroup("Logging")]
        [ShowIf(nameof(overrideLogsSettings))]
#endif
        public StackTraceLogType exceptionLevel = StackTraceLogType.ScriptOnly;
        
#if ODIN_INSPECTOR
        [FoldoutGroup("Logging")]
        [ShowIf(nameof(overrideLogsSettings))]
#endif
        public StackTraceLogType assertLevel = StackTraceLogType.ScriptOnly;
        
        [Tooltip("Build Arguments")]
#if ODIN_INSPECTOR
        [BoxGroup("Build Arguments")]
#endif
#if ODIN_INSPECTOR || TRI_INSPECTOR
        [PropertySpace(8)]
        [HideLabel]
        [InlineProperty]
#endif
        public ArgumentsMap buildArguments = new();

        public bool IsShownStandaloneSubTarget()
        {
            switch (buildTarget)
            {
                case BuildTarget.StandaloneOSX:
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.StandaloneLinux64:
                case BuildTarget.LinuxHeadlessSimulation:
                case BuildTarget.NoTarget:
                    return true;
            }

            return false;
        }
    }
}