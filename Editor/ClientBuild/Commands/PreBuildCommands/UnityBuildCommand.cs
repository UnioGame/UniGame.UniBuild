namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands
{
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using UnityEngine;

    
    [Serializable]
    public abstract class UnityBuildCommand : ScriptableObject,IUnityBuildCommand
    {
        [SerializeField]
        public bool _isActive = true;

        public bool IsActive => _isActive;
        
        public virtual string Name => this!=null ? name : $"[{GetType().Name}]";
        
        public abstract void Execute(IUniBuilderConfiguration configuration);

        public virtual bool Validate(IUniBuilderConfiguration config) => _isActive;
        
        public virtual void OnValidate() { }
    }
}