namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using System.Collections.Generic;
    using Commands.PreBuildCommands;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UniModules.Editor;
    using Interfaces;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif
    
    [Serializable]
    public class BuildCommandStep
    {
        [Space]
#if  ODIN_INSPECTOR
        [ValueDropdown(nameof(GetBuildCommands))]
        [FoldoutGroup("$GroupLabel")]
        //[HideLabel]
        [ShowIf(nameof(IsUnityCommandInitialized))]
        [InlineEditor()]
#endif
        public UnityBuildCommand buildCommand = null;

        [Space] 
        [SerializeReference] 
#if  ODIN_INSPECTOR
        [FoldoutGroup("$GroupLabel")]
        [HideLabel]
        [ShowIf(nameof(IsSerializedCommandInitialized))]
        [InlineProperty]
#endif
        public IUnityBuildCommand serializableCommand = null;

        public string GroupLabel => buildCommand !=null
            ? buildCommand.Name 
            : serializableCommand!=null && serializableCommand is not EmptyBuildCommand 
                ? serializableCommand.Name : "command";

        public bool IsEmptySerializedEmpty => serializableCommand is null or EmptyBuildCommand;
        
        public bool IsUnityCommandInitialized => buildCommand != null || (buildCommand == null && IsEmptySerializedEmpty);
        

        public bool IsSerializedCommandInitialized => (serializableCommand != null && serializableCommand is not EmptyBuildCommand) ||
                                                      (buildCommand == null && IsEmptySerializedEmpty);
 
        
        public IEnumerable<IUnityBuildCommand> GetCommands()
        {
            if (buildCommand != null)
                yield return buildCommand;
            if (serializableCommand != null)
                yield return serializableCommand;
        }
        
        public IEnumerable<UnityBuildCommand> GetBuildCommands()
        {
            return AssetEditorTools.GetAssets<UnityBuildCommand>();
        }
    }
    
    [Serializable]
    public class EmptyBuildCommand : IUnityBuildCommand
    {
        public bool Validate(IUniBuilderConfiguration config)
        {
            return true;
        }

        public bool IsActive => false;

        public string GroupLabel => Name;

        public string Name => nameof(EmptyBuildCommand);
        
        public void Execute(IUniBuilderConfiguration configuration)
        {
            
        }
    }
}