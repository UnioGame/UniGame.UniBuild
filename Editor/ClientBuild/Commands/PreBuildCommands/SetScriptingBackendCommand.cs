﻿namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands
{
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
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
            
            switch (buildParameters.buildTargetGroup) {
                case BuildTargetGroup.Standalone:
                case BuildTargetGroup.Android:
                    PlayerSettings.SetScriptingBackend(buildParameters.buildTargetGroup,scriptingBackend);
                    Debug.Log($"Set ScriptingBackend: {scriptingBackend}");
                    return;
            }
            
            
        }
    }
}
