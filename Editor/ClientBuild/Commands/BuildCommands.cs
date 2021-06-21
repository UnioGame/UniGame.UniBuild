
namespace UniModules.UniGame.UniBuild
{
    using System;
    using System.Collections.Generic;
    using Editor.ClientBuild.Abstract;
    using Editor.ClientBuild.BuildConfiguration;
    using Editor.ClientBuild.Interfaces;
    using UnityEngine;

    [Serializable]
    public class BuildCommands : IBuildCommands
    {
        #region inspector
    
#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Searchable]
#endif
        [Space]
        public List<BuildCommandStep> commands = new List<BuildCommandStep>();

        #endregion

        public IEnumerable<IUnityBuildCommand> Commands => FilterActiveCommands(commands);

        private IEnumerable<IUnityBuildCommand> FilterActiveCommands(IEnumerable<BuildCommandStep> filteredCommands)
        {
            var items = new List<IUnityBuildCommand>();

            foreach (var command in filteredCommands) {
                items.AddRange(command.GetCommands());
            }
            
            return items;
        }

    }
}
