namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild
{
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using UnityEditor;

    [Serializable]
    public class BuildParameters : IBuildParameters {

        public const string BuildFolder = "Build";
        
        public BuildTarget buildTarget;
        public BuildTargetGroup buildTargetGroup;

        public string projectId = string.Empty;
        public int buildNumber = 0;
        public string outputFolder = "Build";
        public string outputFile = "artifact";
        public BuildOptions buildOptions = BuildOptions.None;
        public List<EditorBuildSettingsScene> scenes = new List<EditorBuildSettingsScene>();
        
        public string bundleId = string.Empty;
            
        //Android
        public string keyStorePath;
        public string keyStorePass;
        public string keyStoreAlias;
        public string keyStoreAliasPass;
        public string branch = null;
        public BuildEnvironmentType environmentType = BuildEnvironmentType.Custom;

#region public properties
        
        public BuildTarget BuildTarget => this.buildTarget;

        public BuildTargetGroup BuildTargetGroup => buildTargetGroup;

        public int BuildNumber => this.buildNumber;
        public string OutputFile
        {
            get {
                return this.outputFile;
            }
            set {
                this.outputFile = value;
            }
        }
        
        public string OutputFolder => this.outputFolder;       
        public BuildOptions BuildOptions => this.buildOptions;
        
        public string ProjectId => projectId;
        
        public string BundleId => bundleId;
        
        public BuildEnvironmentType EnvironmentType => environmentType;
        
        public string Branch => branch;

        public IReadOnlyList<EditorBuildSettingsScene> Scenes => scenes;

        #endregion

        public BuildParameters(BuildTarget target, 
            BuildTargetGroup targetGroup,
            IArgumentsProvider arguments)
        {
            
            buildTarget      = target;
            buildTargetGroup = targetGroup;

            if (arguments.GetIntValue(BuildArguments.BuildNumberKey, out var version))
                buildNumber = version;

            arguments.GetStringValue(BuildArguments.BuildOutputFolderKey,
                out var folder, BuildFolder);
            outputFolder = folder;

            arguments.GetStringValue(BuildArguments.BuildOutputNameKey, out var file, string.Empty);
            outputFile = file;
        }
        
        public void SetBuildOptions(BuildOptions targetBuildOptions, bool replace = true)
        {
            
            if (replace) {
                buildOptions = targetBuildOptions;
                return;
            }

            buildOptions |= targetBuildOptions;
            
        }
        
    }
}