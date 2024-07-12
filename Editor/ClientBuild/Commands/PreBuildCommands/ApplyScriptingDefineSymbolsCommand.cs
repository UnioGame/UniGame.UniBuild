namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using UnityEditor;
    using UnityEditor.Build;
    using UnityEngine;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif

    [Serializable]
    public class ApplyScriptingDefineSymbolsCommand : UnitySerializablePreBuildCommand
    {
        private const string DefinesSeparator = ";";

        [SerializeField]
        public string definesKey = "-defineValues";

        [SerializeField]
        public List<string> defaultDefines = new List<string>();

        [SerializeField]
        public List<string> removeDefines = new List<string>();

        public override void Execute(IUniBuilderConfiguration configuration)
        {
            if (!configuration.Arguments.GetStringValue(definesKey, out var defineValues))
            {
                defineValues = string.Empty;
            }

            Execute(defineValues);
        }

        public void Execute(List<string> addKeys, List<string> removeKeys, string definesValue = "")
        {
            var activeBuildGroup = EditorUserBuildSettings.selectedBuildTargetGroup;
            var namedGroup = NamedBuildTarget.FromBuildTargetGroup(activeBuildGroup);
            PlayerSettings.GetScriptingDefineSymbols(namedGroup, out var symbolsValue);

            var origin = symbolsValue.ToArray();
            var symbols = symbolsValue;
            var buildDefines = definesValue.Split(new[] { DefinesSeparator }, StringSplitOptions.None);

            var defines = new List<string>(symbols.Length + buildDefines.Length + addKeys.Count);

            defines.AddRange(symbols);
            defines.AddRange(buildDefines);
            defines.AddRange(addKeys);
            defines.RemoveAll(removeKeys.Contains);
            defines.RemoveAll(string.IsNullOrEmpty);

            defines = defines.Distinct().ToList();

            if (defines.Count == 0) return;

            if (origin.All(defines.Contains) && defines.All(origin.Contains))
                return;

            var definesBuilder = new StringBuilder(300);

            foreach (var define in defines)
            {
                definesBuilder.Append(define);
                definesBuilder.Append(DefinesSeparator);
            }

            PlayerSettings.SetScriptingDefineSymbols(namedGroup, definesBuilder.ToString());
        }

        public void Execute(string defineValues)
        {
            Execute(defaultDefines, removeDefines, defineValues);
        }

#if ODIN_INSPECTOR || TRI_INSPECTOR
        [Button]
#endif
        public void Execute() => Execute(string.Empty);
    }
}