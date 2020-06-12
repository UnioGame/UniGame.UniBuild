namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// update current project version
    /// </summary>
    [CreateAssetMenu(menuName = "UnityBuild/PreBuildCommands/Set Scripting Backend", fileName = "SetScriptingBackendCommand")]
    public class SetScriptingBackendCommand : UnityPreBuildCommand
    {
        [SerializeField]
        private string l2cppEnabled = "-l2cppEnabled";
        
        public override void Execute(IUniBuilderConfiguration configuration)
        {
            var arguments = configuration.Arguments;
            var buildParameters = configuration.BuildParameters;
            
            var scriptingBackend = arguments.Contains(l2cppEnabled) ? 
                ScriptingImplementation.IL2CPP : 
                ScriptingImplementation.Mono2x;
            
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
