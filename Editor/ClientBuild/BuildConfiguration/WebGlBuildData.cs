namespace UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using UnityEditor;
    using UnityEngine;
    
#if UNITY_WEBGL
    using UnityEditor.WebGL;
#endif

    [Serializable]
    public class WebGlBuildData
    {
        public bool ShowDiagnostics = false;
        public Vector2Int Resolution = new(1080,1920);
        public int MaxMemorySize = 1024;
        public bool DataCaching = true;
        public WebGLCompressionFormat CompressionFormat = WebGLCompressionFormat.Brotli;
        
#if UNITY_WEBGL
        public WasmCodeOptimization CodeOptimization = WasmCodeOptimization.BuildTimes;
#endif
    }
}