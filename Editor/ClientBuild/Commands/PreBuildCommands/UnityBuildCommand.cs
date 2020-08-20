namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using Interfaces;
    using UnityEngine;

    
    
    [Serializable]
    public abstract class UnityBuildCommand : ScriptableObject,IUnityBuildCommand
    {
        [SerializeField]
        public bool _isActive = true;

        public bool IsActive => _isActive;
        public string Name => name;
        
        public virtual bool Validate(IUniBuilderConfiguration config) => _isActive;
    }
}