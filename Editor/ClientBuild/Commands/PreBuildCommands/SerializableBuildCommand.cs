namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands
{
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using Sirenix.OdinInspector;
    using UniModules.Editor;
    using UnityEngine;

    [Serializable]
    public abstract class SerializableBuildCommand : IUnityBuildCommand
    {
#if ODIN_INSPECTOR
        [InlineButton(nameof(OpenScript),SdfIconType.Folder2Open)]
#endif
#if TRI_INSPECTOR || ODIN_INSPECTOR
        [GUIColor("GetButtonColor")]
#endif
        [SerializeField]
        public bool isActive = true;
        
        public bool IsActive => isActive;

        public virtual string Name => GetType().Name;
        
        public virtual bool Validate(IUniBuilderConfiguration config) => isActive;
        
        public abstract void Execute(IUniBuilderConfiguration buildParameters);
        
#if TRI_INSPECTOR
        [Button]
#endif
        public void OpenScript()
        {
            GetType().OpenScript();
        }
        
        private Color GetButtonColor()
        {
            return isActive ? 
                new Color(0.2f, 1f, 0.2f) : 
                new Color(1, 0.6f, 0.4f);
            return Color.green;
        }
    }
}