using System;
using System.Collections.Generic;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Abstract
{
    public interface IBuildCommands
    {
        IEnumerable<IUnityBuildCommand> Commands { get; }
        
    }
}