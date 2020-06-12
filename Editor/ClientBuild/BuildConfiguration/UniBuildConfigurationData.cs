namespace UniModules.UniGame.UnityBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using System.Collections.Generic;
    using UnityEditor;
    using UnityEngine;

    [Serializable]
    public class UniBuildConfigurationData : IUniBuildConfigurationData
    {

        [SerializeField]
        private string _artifactName = string.Empty;
        [SerializeField]
        private BuildTarget _buildTarget;
        [SerializeField]
        private BuildTargetGroup _buildTargetGroup;
        [SerializeField]
        private bool _cloudBuild = false;

        public bool CloudBuild => _cloudBuild;

        public BuildTargetGroup BuildTargetGroup => _buildTargetGroup;

        public BuildTarget BuildTarget => _buildTarget;

        public string ArtifactName => _artifactName;
    }
}