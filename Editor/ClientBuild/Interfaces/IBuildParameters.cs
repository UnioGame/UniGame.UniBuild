namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces {
    using System.Collections.Generic;
    using UnityEditor;

    public interface IBuildParameters {
        
        BuildTarget BuildTarget { get; }

        BuildTargetGroup BuildTargetGroup { get; }

        int BuildNumber { get; }
        string OutputFile { get; set; }
        string OutputFolder { get; }
        BuildOptions BuildOptions { get; }

        string ProjectId { get; }
        string BundleId { get; }

        BuildEnvironmentType EnvironmentType { get; }
        string Branch { get; }

        /// <summary>
        /// Player Build scenes
        /// </summary>
        IReadOnlyList<EditorBuildSettingsScene> Scenes { get; }

        void SetBuildOptions(BuildOptions targetBuildOptions, bool replace = true);
    }
}