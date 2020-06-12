namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UnityBuild/PreBuildCommands/Apply Scripting Define Symbols", fileName = "ApplyScriptingDefineSymbols")]
    public class ApplyScriptingDefineSymbolsCommand : UnityPreBuildCommand
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
                return;
            }

            var activeBuildGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var symbolsValue = PlayerSettings.GetScriptingDefineSymbolsForGroup(activeBuildGroup);
            
            var symbols = symbolsValue.Split(new []{DefinesSeparotor},StringSplitOptions.None);
            var buildDefines = defineValues.Split(new []{DefinesSeparotor},StringSplitOptions.None);

            var defines = new List<string>(symbols.Length + buildDefines.Length + defaultDefines.Count);
            defines.AddRange(symbols);
            defines.AddRange(buildDefines);
            defines.AddRange(defaultDefines);
            
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
        
        
    }
}
