namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using Interfaces;
    using UnityEngine;

    [Serializable]
    public abstract class SerializableBuildItem
    {
        [SerializeField]
        public bool isActive = true;

        public bool IsActive => isActive;
        
        public string Name => this.GetType().Name;
        
        public virtual bool Validate(IUniBuilderConfiguration config) => isActive;

    }
}