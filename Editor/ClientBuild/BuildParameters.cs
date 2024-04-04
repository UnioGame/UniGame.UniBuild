namespace UniModules.UniGame.UniBuild.Editor.ClientBuild
{
    using System;
    using System.Collections.Generic;
    using BuildConfiguration;
    using global::UniGame.UniBuild.Editor.ClientBuild;
    using global::UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using UniCore.Runtime.Utils;
    using UniModules.Editor;
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEngine.Serialization;

    [Serializable]
    public class BuildParameters {

        public const string BuildFolder = "Build";
        
        public BuildTarget buildTarget;
        public BuildTargetGroup buildTargetGroup;
        public StandaloneBuildSubtarget standaloneBuildSubtarget;
        public ScriptingImplementation scriptingImplementation = ScriptingImplementation.Mono2x;
        public bool developmentBuild;
        public bool autoconnectProfiler;
        public bool deepProfiling;
        public bool scriptDebugging;
        
        public string                         projectId    = string.Empty;
        public int                            buildNumber  = 0;
        public string                         outputFolder = "Build";
        public string                         outputFile   = "artifact";
        public string                         artifactPath;
        public BuildOptions                   buildOptions = BuildOptions.None;
        public List<EditorBuildSettingsScene> scenes       = new List<EditorBuildSettingsScene>();
        
        public string bundleId = string.Empty;
        public string bundleVersion = string.Empty;
            
        //Android
        public string keyStorePath;
        public string keyStorePass;
        public string keyStoreAlias;
        public string keyStoreAliasPass;
        public string branch = null;
        
        public BuildEnvironmentType environmentType = BuildEnvironmentType.Custom;

        public BuildParameters(UniBuildConfigurationData buildData, IArgumentsProvider arguments)
        {
            var buildArguments = buildData.buildArguments;
            if (buildArguments.isEnable)
            {
                var args = buildData.buildArguments.arguments;
                foreach (var argument in args)
                {
                    if(arguments.Contains(argument.Key)) continue;
                    arguments.SetArgument(argument.Key,argument.Value.Value);
                }
            }
            
            UpdateArguments(arguments);

            if (buildArguments.isEnable)
            {
                foreach (var argument in buildData.buildArguments.arguments)
                {
                    if (argument.Value.Override)
                        arguments.SetArgument(argument.Key, argument.Value.Value);
                }
            }

            buildTarget      = buildData.buildTarget;
            buildTargetGroup = buildData.buildTargetGroup;
            standaloneBuildSubtarget = buildData.standaloneBuildSubTarget;
            scriptingImplementation = buildData.scriptingImplementation;
            
            developmentBuild = buildData.developmentBuild;
            autoconnectProfiler = buildData.autoconnectProfiler;
            deepProfiling = buildData.deepProfiling;
            scriptDebugging = buildData.scriptDebugging;
            
            var namedTarget = standaloneBuildSubtarget is StandaloneBuildSubtarget.Player or StandaloneBuildSubtarget.NoSubtarget
                ? NamedBuildTarget.Standalone
                : NamedBuildTarget.Server;

            PlayerSettings.SetScriptingBackend(namedTarget, scriptingImplementation);
            PlayerSettings.bundleVersion = bundleVersion;
            
            EditorUserBuildSettings.development = developmentBuild;
            EditorUserBuildSettings.connectProfiler = autoconnectProfiler;
            EditorUserBuildSettings.buildWithDeepProfilingSupport = deepProfiling;
            EditorUserBuildSettings.allowDebugging = scriptDebugging;

            if (developmentBuild)
            {
                SetBuildOptions(BuildOptions.Development, false);
            }
            if (autoconnectProfiler)
            {
                SetBuildOptions(BuildOptions.ConnectWithProfiler, false);
            }
            if (deepProfiling)
            {
                SetBuildOptions(BuildOptions.EnableDeepProfilingSupport, false);
            }
            if (scriptDebugging)
            {
                SetBuildOptions(BuildOptions.AllowDebugging, false);
            }
            
            var file   = outputFile;
            var folder = outputFolder;
            var resultArtifactPath = folder.CombinePath(file);
            artifactPath = resultArtifactPath;
        }
        
        public void UpdateArguments(IArgumentsProvider arguments)
        {
            if(arguments.GetStringValue(BuildArguments.GitBranchKey,out var branchValue))
                branch = branchValue;

            bundleVersion = PlayerSettings.bundleVersion;
            if(arguments.GetStringValue(BuildArguments.BundleVersionKey,out var bundleVersionValue))
                bundleVersion = branchValue;
            
            if (arguments.GetStringValue(BuildArguments.Linux64BuildTargetKey, out var linuxPath))
            {
                buildTarget = BuildTarget.StandaloneLinux64;
                buildTargetGroup = BuildTargetGroup.Standalone;
                outputFolder = linuxPath;
            }
            
            if (arguments.GetStringValue(BuildArguments.LinuxUniversalBuildTargetKey, out var universalPath))
            {
                buildTarget = BuildTarget.StandaloneLinux64;
                buildTargetGroup = BuildTargetGroup.Standalone;
                outputFolder = universalPath;
            }
            
            if (arguments.GetStringValue(BuildArguments.Windows64PlayerBuildTargetKey, out var windowsPath))
            {
                buildTarget = BuildTarget.StandaloneWindows64;
                buildTargetGroup = BuildTargetGroup.Standalone;
                outputFolder = windowsPath;
            }

            standaloneBuildSubtarget = EditorUserBuildSettings.standaloneBuildSubtarget;
            if (arguments.GetEnumValue<StandaloneBuildSubtarget>(
                    BuildArguments.StandaloneBuildSubTargetKey,out var subTarget))
            {
                standaloneBuildSubtarget = subTarget;
                EditorUserBuildSettings.standaloneBuildSubtarget = subTarget;
            }
            
            if (arguments.GetEnumValue<BuildEnvironmentType>(
                    BuildArguments.BuildEnvironmentType,out var buildEnvironmentValue))
            {
                environmentType = buildEnvironmentValue;
            }

            if (arguments.GetEnumValue<BuildTarget>(BuildArguments.BuildTargetKey,
                    out var buildTargetValue))
            {
                buildTarget = buildTargetValue;
            }
            
            if (arguments.GetEnumValue<BuildTargetGroup>(BuildArguments.BuildTargetGroupKey,
                    out var buildTargetGroupValue))
            {
                buildTargetGroup = buildTargetGroupValue;
            }
            
            if (arguments.GetStringValue(BuildArguments.BundleIdKey,
                    out var bundleIdValue))
            {
                bundleId = bundleIdValue;
            }
            
            if (arguments.GetStringValue(BuildArguments.BuildOutputNameKey,
                    out var outputFileValue))
            {
                outputFile = outputFileValue;
            }
            
            if(arguments.GetEnumValue<ScriptingImplementation>(BuildArguments.ScriptingImplementationKey,out var scripting))
                scriptingImplementation = scripting;
            
            if (arguments.GetIntValue(BuildArguments.BuildNumberKey, out var version))
                buildNumber = version;

            arguments.GetStringValue(BuildArguments.BuildOutputFolderKey,
                out var folder, BuildFolder);
            outputFolder = folder;

            arguments.GetStringValue(BuildArguments.BuildOutputNameKey, out var file, string.Empty);
            outputFile = file;
            
            arguments.SetValue(BuildArguments.BuildNumberKey, buildNumber.ToString());
            arguments.SetValue(BuildArguments.BuildOutputFolderKey, outputFolder);
            arguments.SetValue(BuildArguments.BuildOutputNameKey, outputFile);
            
            //EditorUserBuildSettings.SwitchActiveBuildTarget(buildTargetGroup,buildTarget);
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