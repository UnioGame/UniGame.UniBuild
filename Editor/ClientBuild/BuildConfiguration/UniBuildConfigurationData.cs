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
        [FormerlySerializedAs("_artifactName")]
        [SerializeField]
        public string artifactName = string.Empty;
        
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

        public bool developmentBuild;

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf("developmentBuild")]
#endif
        public bool autoconnectProfiler;

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf("developmentBuild")]
#endif
        public bool deepProfiling;

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf("developmentBuild")]
#endif
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