namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using Interfaces;
    using UnityEngine;

    public abstract class UnityBuildCommand : ScriptableObject,IUnityBuildCommand
    {
        [SerializeField]
        public bool _isActive = true;

        public bool IsActive => _isActive;
        public string Name => name;
        
        public virtual bool Validate(IUniBuilderConfiguration config) => _isActive;
    }
}