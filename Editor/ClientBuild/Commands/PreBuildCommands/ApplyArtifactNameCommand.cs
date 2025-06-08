namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands
{
    using System;
    using System.IO;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UniModules.UniGame.GitTools.Runtime;
    using UnityEditor;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif
    
    [Serializable]
    public class ApplyArtifactNameCommand : UnitySerializablePreBuildCommand
    {
        private const string nameFormatTemplate = "{0}-{1}";

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [BoxGroup(nameof(artifactName))]
#endif
        public bool useVersionAsArtifactName = false;

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [BoxGroup(nameof(artifactName))]
        [ShowIf(nameof(ShowUseProductName))]
#endif
        public bool useProductNameAsArtifactName = false;
        
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [BoxGroup(nameof(artifactName))]
        [ShowIf(nameof(ShowOverrideArtifactName))]
#endif
        public bool overrideArtifactName = false;

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [BoxGroup(nameof(artifactName))]
        [ShowIf(nameof(ShowOverrideArtifactName))]
#endif
        public string artifactName = string.Empty;
        
        public bool includeGitBranch = false;
        public bool includeBundleVersion = true;
        public bool useNameTemplate = false;

        public string artifactNameTemplate = string.Empty;
        
        public bool overrideExtension = false;
        
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [ShowIf(nameof(overrideExtension))]
#endif
        [Header("Optional: Extension: use '.' before file extension")]
        public string artifactExtension = "";
        
        public bool ShowOverrideArtifactName => !useVersionAsArtifactName && 
                                            overrideArtifactName;
        
        public bool ShowUseProductName => !useVersionAsArtifactName && 
                                          !overrideArtifactName;
        
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
            var outputName = fileName;
            
            if (useVersionAsArtifactName)
            {
                outputName = Application.version;
            }
            else if (overrideArtifactName)
            {
                outputName = artifactName;
            }
            else if (useProductNameAsArtifactName)
            {
                outputName = productName;
            }

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

            outputName = outputName.Replace(":", "_");
            outputName = outputName.Replace(" ", "_");
            outputName += $"{outputExtension}";

            var message = $"[{nameof(ApplyArtifactNameCommand)}] Artifact name: {outputName}";
            Debug.Log(message);
            BuildLogger.Log(message);
            
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