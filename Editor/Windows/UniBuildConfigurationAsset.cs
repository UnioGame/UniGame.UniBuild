#if ODIN_INSPECTOR

using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UniModules.Editor;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
using UnityEngine;

public class UniBuildConfigurationAsset : ScriptableObject
{
    #region public properties
        
        
    [Searchable]
    [ListDrawerSettings(Expanded = true)]
    [InlineProperty]
    public List<UniBuildItem> configurations = new List<UniBuildItem>();

    #endregion
    
    [Button]
    public void Refresh()
    {
        configurations.Clear();
        var configs = AssetEditorTools
            .GetAssets<UniBuildCommandsMap>()
            .Select(x => new UniBuildItem()
            {
                buildCommands = x
            });
        
        configurations.AddRange(configs);
    }

    private void OnEnable()
    {
        Refresh();
    }
}

[Serializable]
[InlineProperty]
[HideLabel]
public class UniBuildItem
{
    [HorizontalGroup(nameof(UniBuildItem))]
    [InlineEditor()]
    public UniBuildCommandsMap buildCommands;

    [Button]
    public void Build()
    {
        buildCommands.ExecuteBuild();
    }
}

#endif