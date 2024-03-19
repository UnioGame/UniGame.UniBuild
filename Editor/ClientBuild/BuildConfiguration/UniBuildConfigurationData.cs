namespace UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using Sirenix.OdinInspector;
    using UniModules.UniGame.UniBuild;
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.Serialization;

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

        [PropertySpace(8)]
        [Tooltip("Build Arguments")]
        [BoxGroup("Build Arguments")]
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