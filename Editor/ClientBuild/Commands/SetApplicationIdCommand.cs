using System;
using UniGame.UniBuild.Editor.ClientBuild.Interfaces;
using UniGame.UniBuild.Editor.Commands.PreBuildCommands;

namespace Game.Modules.UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands
{
    using UnityEditor;
    
#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif
    
    [Serializable]
    public class SetApplicationIdCommand : UnityBuildCommand
    {
        public string applicationId = "com.company.product";
        
        public override void Execute(IUniBuilderConfiguration configuration)
        {
            Execute();
        }

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [Button]
#endif
        public void Execute()
        {
            PlayerSettings.applicationIdentifier = applicationId;
        }
    }
}
