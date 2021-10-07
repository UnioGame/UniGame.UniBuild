namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using Abstract;
    using UnityEditor;
    using UnityEngine;

    [Serializable]
    public class UniBuildConfigurationData : IUniBuildConfigurationData
    {

        [SerializeField]
        public string _artifactName = string.Empty;
        [SerializeField]
        public BuildTarget _buildTarget;
        [SerializeField]
        public BuildTargetGroup _buildTargetGroup;

        public BuildTargetGroup BuildTargetGroup => _buildTargetGroup;

        public BuildTarget BuildTarget => _buildTarget;

        public string ArtifactName => _artifactName;
    }
}