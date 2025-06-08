namespace UniGame.UniBuild.Editor.Interfaces {
    using System.Collections.Generic;
    using UnityEditor;

    public interface IBuildParameters {
        
        BuildTarget BuildTarget { get; }

        BuildTargetGroup BuildTargetGroup { get; }

        int          BuildNumber  { get; set; }
        string       OutputFile   { get; set; }
        string       OutputFolder { get; set; }
        
        string ArtifactPath { get; set; }
        
        BuildOptions BuildOptions { get; }

        string ProjectId { get; }
        string BundleId { get; }

        BuildEnvironmentType EnvironmentType { get; }
        
        string Branch { get;set;  }

        /// <summary>
        /// Player Build scenes
        /// </summary>
        IReadOnlyList<EditorBuildSettingsScene> Scenes { get; }

        void SetBuildOptions(BuildOptions targetBuildOptions, bool replace = true);
    }
}