using UnityEditor.Build.Reporting;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;

    [Serializable]
    public class EditorBuildConfiguration : IUniBuilderConfiguration
    {
        private readonly IArgumentsProvider arguments;
        private readonly BuildParameters   buildParameters;
        private BuildReport _buildReport;

        public EditorBuildConfiguration(IArgumentsProvider argumentsProvider, BuildParameters parameters)
        {
            arguments       = argumentsProvider;
            buildParameters = parameters;
        }
    
        public IArgumentsProvider Arguments => arguments;

        public BuildParameters BuildParameters => buildParameters;

        public BuildReport BuildReport
        {
            get => _buildReport;
            set => _buildReport = value;
        }
    }
}
