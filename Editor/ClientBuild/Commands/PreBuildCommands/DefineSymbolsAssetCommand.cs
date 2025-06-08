namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands
{
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using UnityEngine;
    
#if ODIN_INSPECTOR
     using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif
    
    [Serializable]
    [CreateAssetMenu(menuName = "UniBuild/Commands/DefineSymbolsAssetCommand",fileName = nameof(DefineSymbolsAssetCommand))]
    public class DefineSymbolsAssetCommand : UnityPreBuildCommand
    {
        [SerializeField]
#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [InlineProperty]
        [HideLabel]
#endif
        private ApplyScriptingDefineSymbolsCommand _command = new ApplyScriptingDefineSymbolsCommand();

        public override void Execute(IUniBuilderConfiguration configuration)
        {
            _command.Execute(configuration);
        }

#if ODIN_INSPECTOR || TRI_INSPECTOR
        [Button("Apply Defines")]
#endif
        public void Execute() => _command.Execute(string.Empty);
        
    }
}
