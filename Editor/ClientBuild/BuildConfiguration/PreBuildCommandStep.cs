namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using Commands.PreBuildCommands;
    using Interfaces;

    [Serializable]
    public class PreBuildCommandStep : BuildCommandStep<UnityPreBuildCommand,IUnityPreBuildCommand>
    {
   
    }
}