namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using Interfaces;
    using Parsers;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// https://docs.unity3d.com/ScriptReference/BuildOptions.html
    /// any build parameter and be used by template "-[BuildOptions Item]"
    /// </summary>
    [CreateAssetMenu(menuName = "UnityBuild/PreBuildCommands/Apply BuildOptions", fileName = "BuildOptionsCommand")]
    public class BuildOptionsCommand : UnityPreBuildCommand
    {

        public BuildOptions BuildOptions { get; protected set; }

        public override void Execute(IUniBuilderConfiguration configuration)
        {
            BuildOptions = BuildOptions.None;
            
            var enumBuildOptionsParser = new EnumArgumentParser<BuildOptions>();
            var buildOptions = enumBuildOptionsParser.Parse(configuration.Arguments);
            var options = BuildOptions.None;
            
            for (int i = 0; i < buildOptions.Count; i++) {
                options |= buildOptions[i];
            }

            BuildOptions = options;
            
            configuration.BuildParameters.SetBuildOptions(options,false);
        }
        
    }
}
