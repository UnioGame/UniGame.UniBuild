using UnityEditor.Build.Reporting;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using Interfaces;

    [Serializable]
    public class EditorBuildConfiguration : IUniBuilderConfiguration
    {
        private readonly IArgumentsProvider arguments;
        private readonly IBuildParameters   buildParameters;
        private BuildReport _buildReport;

        public EditorBuildConfiguration(IArgumentsProvider argumentsProvider, IBuildParameters parameters)
        {
            arguments       = argumentsProvider;
            buildParameters = parameters;
        }
    
        public IArgumentsProvider Arguments => arguments;

        public IBuildParameters BuildParameters => buildParameters;

        public BuildReport BuildReport
        {
            get => _buildReport;
            set => _buildReport = value;
        }
    }
}
