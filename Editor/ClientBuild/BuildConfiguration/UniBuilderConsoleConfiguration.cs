namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.BuildConfiguration
{
    using Extensions;
    using Interfaces;
    using Parsers;
    using UnityEditor;
    using UnityEngine;

    public class UniBuilderConsoleConfiguration : IUniBuilderConfiguration
    {
        private EnumArgumentParser<BuildTarget> buildTargetParser;
        private EnumArgumentParser<BuildTargetGroup> buildTargetGroupParser;
        
        private ArgumentsProvider argumentsProvider;
        private BuildParameters buildParameters;
        
        public UniBuilderConsoleConfiguration(string[] commandLineArgs)
        {

            
            argumentsProvider = new ArgumentsProvider(commandLineArgs);

            var buildTarget      = argumentsProvider.GetBuildTarget();
            var buildTargetGroup = argumentsProvider.GetBuildTargetGroup();

            buildParameters = new BuildParameters(buildTarget,buildTargetGroup, argumentsProvider);

            Debug.Log(argumentsProvider);
            
        }

        public IArgumentsProvider Arguments => argumentsProvider;

        public IBuildParameters BuildParameters => buildParameters;


        
    }
}
