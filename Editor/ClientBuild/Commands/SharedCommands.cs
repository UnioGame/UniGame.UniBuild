using System;
using System.Collections.Generic;
using UniGame.UniBuild.Editor;
using UniGame.UniBuild.Editor.Commands.PreBuildCommands;
using UniGame.UniBuild.Editor.Interfaces;
using UnityEngine;

namespace UniGame.UniBuild.Editor.Commands
{
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using PreBuildCommands;

    [Serializable]
    public class SharedCommands : UnityBuildCommand
    {
        
        public List<BuildCommandStep> commands = new List<BuildCommandStep>();

        public override void Execute(IUniBuilderConfiguration configuration)
        {
            foreach (var command in commands)
            {
                foreach (var buildCommand in command.GetCommands())
                {
                    if (!buildCommand.IsActive)
                    {
                        continue;
                    }
                    buildCommand.Execute(configuration);
                }
            }
        }
    }
}
