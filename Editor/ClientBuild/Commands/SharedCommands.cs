using System;
using System.Collections.Generic;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;
using UnityEngine;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands
{
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
