namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Extensions
{
    using System.Linq;
    using Interfaces;
    using Parsers;
    using UnityEditor;

    public static class ArgumentsProviderExtension 
    {
        private static EnumArgumentParser<BuildTarget>  buildTargetParser = new     EnumArgumentParser<BuildTarget>();
        private static EnumArgumentParser<BuildTargetGroup>  buildTargetGroupParser = new EnumArgumentParser<BuildTargetGroup>();
        
        public static BuildTarget GetBuildTarget(this IArgumentsProvider arguments)
        {
            var targets = buildTargetParser.Parse(arguments);
            return targets.Count > 0 ?
                targets.FirstOrDefault() :
                EditorUserBuildSettings.activeBuildTarget;
        }

        public static BuildTargetGroup GetBuildTargetGroup( this IArgumentsProvider arguments)
        {
            var groups = buildTargetGroupParser.Parse(arguments);
            return groups.Count > 0 ? groups.First() :
                EditorUserBuildSettings.selectedBuildTargetGroup;
        }

    }
}
