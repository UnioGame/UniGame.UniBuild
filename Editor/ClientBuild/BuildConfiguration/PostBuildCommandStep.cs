using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using Interfaces;

    [Serializable]
    public class PostBuildCommandStep : BuildCommandStep<UnityBuildCommand,IUnityBuildCommand>
    {
    
    }
}