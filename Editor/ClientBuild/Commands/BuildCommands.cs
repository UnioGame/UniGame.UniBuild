using System;
using System.Collections.Generic;
using UniModules.UniCore.Runtime.ObjectPool.Runtime;
using UniModules.UniCore.Runtime.ObjectPool.Runtime.Extensions;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Abstract;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;
using UnityEngine;

[Serializable]
public class BuildCommands : IBuildCommands
{
    #region inspector
    
#if ODIN_INSPECTOR
    [Sirenix.OdinInspector.Searchable]
#endif
    [Space]
    public List<BuildCommandStep> commands = new List<BuildCommandStep>();

    #endregion
    
    public IEnumerable<IUnityBuildCommand> Commands => FilterActiveCommands(commands);

    private IEnumerable<IUnityBuildCommand> FilterActiveCommands(IEnumerable<BuildCommandStep> commands)
    {
        foreach (var command in commands) {
            foreach (var buildCommand in command.GetCommands())
            {
                yield return buildCommand;
            }
        }
    }

}
