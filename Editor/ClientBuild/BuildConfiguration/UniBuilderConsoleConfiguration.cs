namespace UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.Extensions;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.Parsers;
    using UnityEditor;
    using UnityEditor.Build.Reporting;
    using UnityEngine;

    public class UniBuilderConsoleConfiguration : IUniBuilderConfiguration
    {
        private EnumArgumentParser<BuildTarget> buildTargetParser;
        private EnumArgumentParser<BuildTargetGroup> buildTargetGroupParser;
        
        private ArgumentsProvider argumentsProvider;
        private BuildParameters buildParameters;
        private BuildReport _buildReport;

        public UniBuilderConsoleConfiguration(string[] commandLineArgs)
        {
            argumentsProvider = new ArgumentsProvider(commandLineArgs);

            var buildTarget      = argumentsProvider.GetBuildTarget();
            var buildTargetGroup = argumentsProvider.GetBuildTargetGroup();

            var buildData = new UniBuildConfigurationData()
            {
                buildTarget = buildTarget,
                buildTargetGroup = buildTargetGroup,
            };
            
            buildParameters = new BuildParameters(buildData, argumentsProvider);

            Debug.Log(argumentsProvider);
        }

        public IArgumentsProvider Arguments => argumentsProvider;

        public BuildParameters BuildParameters => buildParameters;

        public BuildReport BuildReport
        {
            get => _buildReport;
            set => _buildReport = value;
        }
    }
}
