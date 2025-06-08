using System.Collections.Generic;
using UniGame.UniBuild.Editor.Interfaces;

namespace UniGame.UniBuild.Editor.Abstract
{
    using Interfaces;

    public interface IBuildCommands
    {
        IEnumerable<IUnityBuildCommand> Commands { get; }
        
    }
}