using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands;
using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces;

namespace UniModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System.IO;
    using GitTools.Runtime;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UnityBuild/PreBuildCommands/Apply Artifact Name With Version", fileName = "ApplyArtifactNameWithVersion")]
    public class ApplyArtifactNameWithVersionCommand : UnityPreBuildCommand
    {
        private const string nameFormatTemplate = "{0}-{1}";

        public bool useProductName = true;
        public bool includeGitBranch;
        public bool includeBundleVersion = true;
        public bool useNameTemplate = false;
        
        public string artifactNameTemplate = string.Empty;
        [Tooltip("use '.' before file extension")]
        public string artifactExtension = "";
        
        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            var outputFilename = buildParameters.BuildParameters.OutputFile;
            var outputExtension = string.IsNullOrEmpty(artifactExtension)?
                Path.GetExtension(outputFilename) : artifactExtension;
            
            var fileName = Path.GetFileNameWithoutExtension(outputFilename);
            
            var artifactName = useProductName ? 
                PlayerSettings.productName :
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
            buildParameters.BuildParameters.OutputFile = artifactName;
        }
    }
}
