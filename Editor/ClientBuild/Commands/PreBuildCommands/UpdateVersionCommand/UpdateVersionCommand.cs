﻿namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands.UpdateVersionCommand 
{
    using System;
    using System.Text;
    using GitTools.Runtime;
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// update current project version
    /// </summary>
    [Serializable]
    public class UpdateVersionCommand : UnitySerializablePreBuildCommand
    {
        [SerializeField]
        private int minBuildNumber = 0;

        [SerializeField]
        private bool appendBranch = false;
        
        public override void Execute(IUniBuilderConfiguration configuration)
        {

            var buildParameters = configuration.BuildParameters;
            var branch = appendBranch ? configuration.BuildParameters.Branch : null;
            UpdateBuildVersion(buildParameters.BuildTarget, buildParameters.BuildNumber, branch);
            
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Execute()
        {
            var branch = appendBranch ?  GitCommands.GetGitBranch() : string.Empty;
            UpdateBuildVersion(EditorUserBuildSettings.activeBuildTarget, 1, branch);
        }
        
        public void UpdateBuildVersion(BuildTarget buildTarget,int buildNumber, string branch) 
        {
            var buildVersionProvider = new BuildVersionProvider();
            var logBuilder = new StringBuilder(200);

            var currentBuildNumber = buildVersionProvider.GetActiveBuildNumber(buildTarget);
            var activeBuildNumber  = Mathf.Max(buildNumber, minBuildNumber);
            activeBuildNumber = Mathf.Max(1, activeBuildNumber);
            var resultBuildNumber  = currentBuildNumber + activeBuildNumber;
            
            var bundleVersion     = buildVersionProvider.GetBuildVersion(buildTarget, PlayerSettings.bundleVersion, resultBuildNumber, branch);
            
            PlayerSettings.bundleVersion = bundleVersion;
            var buildNumberString =  resultBuildNumber.ToString();
            PlayerSettings.iOS.buildNumber = buildNumberString;
            PlayerSettings.Android.bundleVersionCode = resultBuildNumber;
            
            logBuilder.Append("\t Parameters build number : ");
            logBuilder.Append(buildNumber);
            logBuilder.AppendLine();
 
            logBuilder.Append("\t ResultBuildNumber build number : ");
            logBuilder.Append(resultBuildNumber);
            logBuilder.AppendLine();
                                  
            logBuilder.Append("\t PlayerSettings.bundleVersion : ");
            logBuilder.Append(bundleVersion);
            logBuilder.AppendLine();
            
            logBuilder.Append("\t PlayerSettings.iOS.buildNumber : ");
            logBuilder.Append(buildNumberString);
            logBuilder.AppendLine();
            
            logBuilder.Append("\t PlayerSettings.Android.bundleVersionCode : ");
            logBuilder.Append(resultBuildNumber);
            logBuilder.AppendLine();
            
            Debug.Log(logBuilder);
        }
    }
    
}
