namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Extensions
{
    using System.Linq;
    using global::UniGame.UniBuild.Editor.ClientBuild;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using Parsers;
    using UnityEditor;

    public static class ArgumentsProviderExtension 
    {
        private static EnumArgumentParser<BuildTarget>  buildTargetParser = new     EnumArgumentParser<BuildTarget>();
        private static EnumArgumentParser<BuildTargetGroup>  buildTargetGroupParser = new EnumArgumentParser<BuildTargetGroup>();
        
        public static BuildTarget GetBuildTarget(this IArgumentsProvider arguments)
        {
            if (arguments.GetEnumValue<BuildTarget>(BuildArguments.BuildTargetKey,
                    out var buildTargetValue))
                return buildTargetValue;
            
            var targets = buildTargetParser.Parse(arguments);
            return targets.Count > 0 ?
                targets.FirstOrDefault() :
                EditorUserBuildSettings.activeBuildTarget;
        }

        public static BuildTargetGroup GetBuildTargetGroup( this IArgumentsProvider arguments)
        {
            if (arguments.GetEnumValue<BuildTargetGroup>(BuildArguments.BuildTargetGroupKey,
                    out var buildTargetValue))
                return buildTargetValue;
            
            var groups = buildTargetGroupParser.Parse(arguments);
            return groups.Count > 0 ? groups.First() :
                EditorUserBuildSettings.selectedBuildTargetGroup;
        }

    }
}
