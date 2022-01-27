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
        private const string DefinesSeparator = ";";

        [SerializeField] private string definesKey = "-defineValues";

        [SerializeField] private List<string> defaultDefines = new List<string>();

        [SerializeField] private List<string> removeDefines = new List<string>();

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
            var symbolsValue = PlayerSettings.GetScriptingDefineSymbolsForGroup(activeBuildGroup);

            var origin = symbolsValue.Split(new[] { DefinesSeparator }, StringSplitOptions.None);
            var symbols = symbolsValue.Split(new[] { DefinesSeparator }, StringSplitOptions.None);
            var buildDefines = defineValues.Split(new[] { DefinesSeparator }, StringSplitOptions.None);

            var defines = new List<string>(symbols.Length + buildDefines.Length + defaultDefines.Count);

            defines.AddRange(symbols);
            defines.AddRange(buildDefines);
            defines.AddRange(defaultDefines);
            defines.RemoveAll(x => removeDefines.Contains(x));

            defines = defines.Distinct().ToList();

            if (defines.Count == 0)
                return;

            if (origin.All(defines.Contains) && defines.All(origin.Contains))
                return;

            var definesBuilder = new StringBuilder(300);
            
            foreach (var define in defines)
            {
                definesBuilder.Append(define);
                definesBuilder.Append(DefinesSeparator);
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(activeBuildGroup,  definesBuilder.ToString());
        }

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.Button]
#endif
        public void Execute() => Execute(String.Empty);
    }
}