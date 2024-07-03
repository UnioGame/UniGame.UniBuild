namespace UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration
{
    using System;
    using UnityEditor;
    using UnityEditor.WebGL;
    using UnityEngine;

    [Serializable]
    public class WebGlBuildData
    {
        public bool ShowDiagnostics = false;
        public Vector2Int Resolution = new(1080,1920);
        public int MaxMemorySize = 1024;
        public bool DataCaching = true;
        public WebGLCompressionFormat CompressionFormat = WebGLCompressionFormat.Brotli;
        public WasmCodeOptimization CodeOptimization = WasmCodeOptimization.BuildTimes;
    }
}