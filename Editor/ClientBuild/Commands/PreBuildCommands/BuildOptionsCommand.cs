namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using Interfaces;
    using Parsers;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// https://docs.unity3d.com/ScriptReference/BuildOptions.html
    /// any build parameter and be used by template "-[BuildOptions Item]"
    /// </summary>
    [Serializable]
    public class BuildOptionsCommand : UnitySerializablePreBuildCommand
    {
        [SerializeField]
        public BuildOptions[] _buildOptions = new BuildOptions[] {BuildOptions.None};

        public override void Execute(IUniBuilderConfiguration configuration)
        {
            var enumBuildOptionsParser = new EnumArgumentParser<BuildOptions>();
            var buildOptions = enumBuildOptionsParser.Parse(configuration.Arguments);
            var options = BuildOptions.None;
            
            for (int i = 0; i < buildOptions.Count; i++) {
                options |= buildOptions[i];
            }

            foreach (var buildValue in _buildOptions) {
                options |= buildValue;
            }
            
            configuration.BuildParameters.SetBuildOptions(options,false);
        }
        
    }
}
