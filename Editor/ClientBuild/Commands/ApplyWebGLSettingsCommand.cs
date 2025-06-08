namespace Game.Modules.UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands
{
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using global::UniGame.UniBuild.Editor.Commands.PreBuildCommands;
    
    using UnityEditor;

#if ODIN_INSPECTOR
    using Sirenix.OdinInspector;
#elif TRI_INSPECTOR
    using TriInspector;
#endif
    
    [Serializable]
    public class ApplyWebGLSettingsCommand : SerializableBuildCommand
    {
        public WebGlBuildData webGlBuildData = new();
        
        public override void Execute(IUniBuilderConfiguration configuration)
        {
            Execute();
            configuration.BuildParameters
                .UpdateWebGLData(webGlBuildData);
        }

#if ODIN_INSPECTOR || TRI_INSPECTOR
        [Button]
#endif
        public void Execute()
        {
            UpdateWebGLData(webGlBuildData);
        }
        
        public void UpdateWebGLData(WebGlBuildData data)
        {
            PlayerSettings.WebGL.showDiagnostics = webGlBuildData.ShowDiagnostics;
            PlayerSettings.WebGL.compressionFormat = webGlBuildData.CompressionFormat;
            PlayerSettings.WebGL.memorySize = webGlBuildData.MaxMemorySize;
            PlayerSettings.WebGL.dataCaching = webGlBuildData.DataCaching;
            PlayerSettings.WebGL.debugSymbolMode = webGlBuildData.DebugSymbolMode;
            PlayerSettings.WebGL.exceptionSupport = webGlBuildData.ExceptionSupport;
            PlayerSettings.defaultWebScreenWidth = webGlBuildData.Resolution.x;
            PlayerSettings.defaultWebScreenHeight = webGlBuildData.Resolution.y;
            PlayerSettings.WebGL.linkerTarget = webGlBuildData.LinkerTarget;
            
#if UNITY_WEBGL
            UnityEditor.WebGL.UserBuildSettings.codeOptimization = data.CodeOptimization;
#endif
        }
    }
}