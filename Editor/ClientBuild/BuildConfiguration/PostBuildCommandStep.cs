namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using Commands.PostBuildCommands;
    using Interfaces;

    [Serializable]
    public class PostBuildCommandStep : BuildCommandStep<UnityPostBuildCommand,IUnityPostBuildCommand>
    {
    
    }
}