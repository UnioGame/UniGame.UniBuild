namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands
{
    using System;
    using Interfaces;

    [Serializable]
    public abstract class UnitySerializablePreBuildCommand : SerializableBuildCommand, IUnityPreBuildCommand
    {
    }
    
    
    [Serializable]
    public abstract class UnitySerializablePostBuildCommand :SerializableBuildCommand, IUnityPostBuildCommand
    {
    }
}