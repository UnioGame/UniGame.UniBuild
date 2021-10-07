using System.Collections.Generic;
using Sirenix.OdinInspector;
using UniModules.Editor;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
using UnityEngine;

public class UniBuildConfigurationAsset : ScriptableObject
{
    #region public properties
        
        
    [Searchable]
    [ListDrawerSettings(Expanded = true)]
    [InlineEditor]
    public List<UniBuildCommandsMap> configurations = new List<UniBuildCommandsMap>();

    #endregion
    
    [Button]
    public void Refresh()
    {
        configurations.Clear();
        var configs = AssetEditorTools.GetAssets<UniBuildCommandsMap>();
        configurations.AddRange(configs);
    }

    private void OnEnable()
    {
        Refresh();
    }
}