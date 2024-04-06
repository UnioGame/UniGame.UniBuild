
namespace UniModules.UniGame.UniBuild
{
    using System;
    using System.Collections.Generic;
    using Editor.ClientBuild.Abstract;
    using Editor.ClientBuild.BuildConfiguration;
    using Editor.ClientBuild.Interfaces;
    using UnityEngine;

#if ODIN_INSPECTOR
     using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif
    
    [Serializable]
    public class BuildCommands : IBuildCommands
    {
        private static Color _oddColor = new Color(0.2f, 0.4f, 0.3f);
        
        #region inspector
    
#if ODIN_INSPECTOR
        [Searchable]
        [ListDrawerSettings(ElementColor = nameof(GetElementColor))]//ListElementLabelName = "GroupLabel"
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

        private Color GetElementColor(int index, Color defaultColor)
        {
            var result = index % 2 == 0 
                ? _oddColor : defaultColor;
            return result;
        }
    }
}
