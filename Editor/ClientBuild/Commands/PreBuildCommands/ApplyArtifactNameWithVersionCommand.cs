namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using System.IO;
    using GitTools.Runtime;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UnityEditor;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif
    
    [Serializable]
    public class ApplyArtifactNameWithVersionCommand : UnitySerializablePreBuildCommand
    {
        private const string nameFormatTemplate = "{0}-{1}";

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [BoxGroup(nameof(artifactName))]
#endif
        public bool overrideArtifactName = false;
        
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf(nameof(overrideArtifactName))]
        [BoxGroup(nameof(artifactName))]
#endif
        public bool useProductName = true;
        
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [BoxGroup(nameof(artifactName))]
        [HideIf(nameof(useProductName))]
#endif
        public string artifactName = string.Empty;
        
        public bool includeGitBranch;
        public bool includeBundleVersion = true;
        public bool useNameTemplate = false;

        public string artifactNameTemplate = string.Empty;
        
        public bool overrideExtension = false;
        
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf(nameof(overrideExtension))]
#endif
        [Header("Optional: Extension: use '.' before file extension")]
        public string artifactExtension = "";

        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            var outputFilename = buildParameters.BuildParameters.outputFile;
            var outputName = CreateArtifactLocation(outputFilename, PlayerSettings.productName);
            buildParameters.BuildParameters.outputFile = outputName;
        }

        public string CreateArtifactLocation(string outputFilename, string productName)
        {
            var outputExtension = string.IsNullOrEmpty(artifactExtension) || 
                                  !overrideExtension
                ? GetExtension()
                : artifactExtension;
            
            var fileName = Path.GetFileNameWithoutExtension(outputFilename);
            
            var outputName = overrideArtifactName 
                ? useProductName ? productName : artifactName 
                : fileName;

            if (useNameTemplate)
            {
                outputName = string.Format(artifactNameTemplate, outputName);
            }

            if (includeGitBranch)
            {
                var branch = GitCommands.GetGitBranch();
                if (string.IsNullOrEmpty(branch) == false)
                {
                    outputName = string.Format(nameFormatTemplate, outputName, branch);
                }
            }

            if (includeBundleVersion)
            {
                outputName = string.Format(nameFormatTemplate, outputName, PlayerSettings.bundleVersion);
            }

            outputName = outputName.Replace(":", "");
            outputName += $"{outputExtension}";

            return outputName;
        }

        private string GetExtension()
        {
            var extension = string.Empty;

            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.StandaloneWindows:
                    return extension;
                case BuildTarget.Android:
                    if (EditorUserBuildSettings.exportAsGoogleAndroidProject)
                        return string.Empty;
                    var isAppBundle = EditorUserBuildSettings.buildAppBundle;
                    extension = isAppBundle ? ".aab" : ".apk";
                    break;
                default:
                    return extension;
            }

            return extension;
        }
    }
}