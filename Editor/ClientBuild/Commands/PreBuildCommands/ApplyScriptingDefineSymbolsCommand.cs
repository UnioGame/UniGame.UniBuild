namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    [Serializable]
    public class ApplyScriptingDefineSymbolsCommand : UnitySerializablePreBuildCommand
    {
        [SerializeField]
        private string definesKey = "-defineValues";

        [SerializeField]
        private List<string> defaultDefines = new List<string>();
        
        private const string DefinesSeparotor = ";";
        
        public override void Execute(IUniBuilderConfiguration configuration)
        {
            if (!configuration.Arguments.GetStringValue(definesKey, out var defineValues))
            {
                defineValues = string.Empty;
            }

            Execute(defineValues);
        }

        public void Execute(string defineValues)
        {
            var activeBuildGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var symbolsValue     = PlayerSettings.GetScriptingDefineSymbolsForGroup(activeBuildGroup);
            
            var symbols      = symbolsValue.Split(new []{DefinesSeparotor},StringSplitOptions.None);
            var buildDefines = defineValues.Split(new []{DefinesSeparotor},StringSplitOptions.None);

            var defines = new List<string>(symbols.Length + buildDefines.Length + defaultDefines.Count);
            defines.AddRange(symbols);
            defines.AddRange(buildDefines);
            defines.AddRange(defaultDefines);
            
            defines = defines.Distinct().ToList();
            
            if (defines.Count == 0)
                return;

            var definesBuilder = new StringBuilder(300);
            
            foreach (var define in defines.Distinct())
            {
                definesBuilder.Append(define);
                definesBuilder.Append(DefinesSeparotor);
            }
            
            PlayerSettings.SetScriptingDefineSymbolsForGroup(activeBuildGroup,definesBuilder.ToString());

        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Execute() => Execute(String.Empty);
        
        
    }
}
