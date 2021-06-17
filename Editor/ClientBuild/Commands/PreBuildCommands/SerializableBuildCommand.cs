namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using Interfaces;
    using UnityEngine;

    [Serializable]
    public abstract class SerializableBuildCommand : IUnityBuildCommand
    {
        [SerializeField]
        public bool isActive = true;

        public string name;
        
        public bool IsActive => isActive;
        
        public string Name => string.IsNullOrEmpty(name) ? this.GetType().Name : name;
        
        public virtual bool Validate(IUniBuilderConfiguration config) => isActive;
        
        public abstract void Execute(IUniBuilderConfiguration buildParameters);

    }
}