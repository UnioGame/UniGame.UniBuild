namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using UnityEngine;

    [Serializable]
    [CreateAssetMenu(menuName = "UniBuild/Commands/DefineSymbolsAssetCommand",fileName = nameof(DefineSymbolsAssetCommand))]
    public class DefineSymbolsAssetCommand : UnityPreBuildCommand
    {
        [SerializeField]
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.InlineProperty]
        [Sirenix.OdinInspector.HideLabel]
#endif
        private ApplyScriptingDefineSymbolsCommand _command = new ApplyScriptingDefineSymbolsCommand();

        public override void Execute(IUniBuilderConfiguration configuration)
        {
            _command.Execute(configuration);
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button("Apply Defines")]
#endif
        public void Execute() => _command.Execute(string.Empty);
        
    }
}
