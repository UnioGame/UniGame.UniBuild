namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using Interfaces;
    using UnityEditor.Build.Reporting;

    [Serializable]
    public abstract class UnitySerializablePreBuildCommand : SerializableBuildItem, IUnityPreBuildCommand
    {
        public abstract void Execute(IUniBuilderConfiguration buildParameters);
    }
    
    
    [Serializable]
    public abstract class UnitySerializablePostBuildCommand :SerializableBuildItem, IUnityPostBuildCommand
    {
        public abstract void Execute(IUniBuilderConfiguration configuration,BuildReport buildReport = null);
    }
}