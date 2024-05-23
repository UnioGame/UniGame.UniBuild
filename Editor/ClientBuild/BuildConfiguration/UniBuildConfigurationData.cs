namespace UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using UniModules.UniGame.UniBuild;
    using UnityEditor;
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
        [FormerlySerializedAs("_artifactName")]
        [ShowIf(nameof(overrideArtifactName))]
        public string artifactName = string.Empty;

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

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf(nameof(IsShownStandaloneSubTarget))]
#endif
        public StandaloneBuildSubtarget standaloneBuildSubTarget = StandaloneBuildSubtarget.Player;

        public ScriptingImplementation scriptingImplementation = ScriptingImplementation.Mono2x;

        [Tooltip("development build")]
        public bool developmentBuild;

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf("developmentBuild")]
#endif
        public bool autoconnectProfiler;

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf("developmentBuild")]
#endif
        [Tooltip("enable deep profiling")]
        public bool deepProfiling;

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf("developmentBuild")]
#endif
        [Tooltip("allow script debugging")]
        public bool scriptDebugging;
        
        [Tooltip("Build Arguments")]
#if ODIN_INSPECTOR
        [BoxGroup("Build Arguments")]
#endif
#if  ODIN_INSPECTOR || TRI_INSPECTOR
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