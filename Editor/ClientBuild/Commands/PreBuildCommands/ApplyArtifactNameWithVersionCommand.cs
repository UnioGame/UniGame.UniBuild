namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using System.IO;
    using GitTools.Runtime;
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    [Serializable]
    public class ApplyArtifactNameWithVersionCommand : UnitySerializablePreBuildCommand
    {
        private const string nameFormatTemplate = "{0}-{1}";

        public bool useProductName = true;
        public bool includeGitBranch;
        public bool includeBundleVersion = true;
        public bool useNameTemplate = false;

        public string artifactNameTemplate = string.Empty;
        
        [Header("Optional: Extension: use '.' before file extension")]
        public string artifactExtension = "";
        
        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            var outputFilename = buildParameters.BuildParameters.OutputFile;
            var artifactName = CreateArtifactLocation(outputFilename,PlayerSettings.productName);
            buildParameters.BuildParameters.OutputFile = artifactName;
        }

        public string CreateArtifactLocation(string outputFilename, string productName)
        {
            var outputExtension = 
                string.IsNullOrEmpty(artifactExtension)?
                Path.GetExtension(outputFilename) 
                : artifactExtension;
            
            var fileName = Path.GetFileNameWithoutExtension(outputFilename);
            
            var artifactName = useProductName ? 
                productName :
                fileName;
            
            if (useNameTemplate) {
                artifactName = string.Format(artifactNameTemplate, artifactName);
            }
            
            if (includeGitBranch) {
                var branch = GitCommands.GetGitBranch();
                if (string.IsNullOrEmpty(branch) == false) {
                    artifactName = string.Format(nameFormatTemplate, artifactName,branch);
                }
            }
            
            if (includeBundleVersion) {
                artifactName = string.Format(nameFormatTemplate, artifactName,PlayerSettings.bundleVersion);
            }

            artifactName += $"{outputExtension}";

            return artifactName;
        }

    }

}
