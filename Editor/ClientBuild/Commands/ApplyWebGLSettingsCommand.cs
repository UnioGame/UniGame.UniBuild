namespace Game.Modules.UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands
{
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using global::UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
    
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
            PlayerSettings.WebGL.showDiagnostics = data.ShowDiagnostics;
            PlayerSettings.WebGL.compressionFormat = data.CompressionFormat;
            PlayerSettings.WebGL.memorySize = data.MaxMemorySize;
            PlayerSettings.WebGL.dataCaching = data.DataCaching;
            PlayerSettings.defaultWebScreenWidth = data.Resolution.x;
            PlayerSettings.defaultWebScreenHeight = data.Resolution.y;
            
            UnityEditor.WebGL.UserBuildSettings.codeOptimization = data.CodeOptimization;
        }
    }
}