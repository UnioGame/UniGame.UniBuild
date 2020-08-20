namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// update current project version
    /// </summary>
    [Serializable]
    public class SetScriptingBackendCommand : UnitySerializablePreBuildCommand
    {
        [SerializeField]
        private string l2cppEnabled = "-l2cppEnabled";

        private ScriptingImplementation _defaultBackend = ScriptingImplementation.Mono2x;

        public override void Execute(IUniBuilderConfiguration configuration)
        {
            var arguments = configuration.Arguments;
            var buildParameters = configuration.BuildParameters;
            
            var scriptingBackend = arguments.Contains(l2cppEnabled) ? 
                ScriptingImplementation.IL2CPP : 
                _defaultBackend;
            
#if FORCE_MONO
            scriptingBackend = ScriptingImplementation.Mono2x;
#elif FORCE_IL2CPP
            scriptingBackend = ScriptingImplementation.IL2CPP;
#endif
            
            switch (buildParameters.BuildTargetGroup) {
                case BuildTargetGroup.Standalone:
                case BuildTargetGroup.Android:
                    PlayerSettings.SetScriptingBackend(buildParameters.BuildTargetGroup,scriptingBackend);
                    Debug.Log($"Set ScriptingBackend: {scriptingBackend}");
                    return;
            }
            
            
        }
    }
}
