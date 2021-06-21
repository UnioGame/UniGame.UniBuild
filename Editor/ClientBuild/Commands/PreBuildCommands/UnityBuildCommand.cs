namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using Interfaces;
    using UnityEngine;

    
    
    [Serializable]
    public abstract class UnityBuildCommand : ScriptableObject,IUnityBuildCommand
    {
        [SerializeField]
        public string commandName = String.Empty;
        
        [SerializeField]
        public bool _isActive = true;

        public bool IsActive => _isActive;
        
        public string Name => commandName;
        
        public abstract void Execute(IUniBuilderConfiguration configuration);

        public virtual bool Validate(IUniBuilderConfiguration config) => _isActive;


        public virtual void OnValidate()
        {
            commandName = string.IsNullOrEmpty(commandName) ? name : commandName;
        }
    }
}