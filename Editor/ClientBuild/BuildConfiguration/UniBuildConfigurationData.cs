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

        [ShowIf(nameof(IsShownStandaloneSubTarget))]
        public StandaloneBuildSubtarget standaloneBuildSubTarget = StandaloneBuildSubtarget.Player;

        public ScriptingImplementation scriptingImplementation = ScriptingImplementation.Mono2x;

        public bool developmentBuild;

        [ShowIf("developmentBuild")]
        public bool autoconnectProfiler;

        [ShowIf("developmentBuild")]
        public bool deepProfiling;

        [ShowIf("developmentBuild")]
        public bool scriptDebugging;

        [PropertySpace(8)]
        [Tooltip("Build Arguments")]
#if ODIN_INSPECTOR
        [BoxGroup("Build Arguments")]
#endif
        [HideLabel]
        [InlineProperty]
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