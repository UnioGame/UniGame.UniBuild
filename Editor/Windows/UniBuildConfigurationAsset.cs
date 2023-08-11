#if ODIN_INSPECTOR

using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UniModules.Editor;
using UniModules.UniGame.UniBuild.Editor.ClientBuild.BuildConfiguration;
using UnityEngine;

[CreateAssetMenu(menuName = "UniGame/UniBuild/UniBuild ConfigurationAsset", fileName ="UniBuildConfigurationAsset")]
public class UniBuildConfigurationAsset : ScriptableObject
{
    #region public properties
        
        
    [Searchable]
    [ListDrawerSettings(Expanded = true)]
    [InlineProperty]
    public List<UniBuildItem> configurations = new List<UniBuildItem>();

    #endregion
    
    [Button]
    [OnInspectorInit]
    public UniBuildConfigurationAsset Refresh()
    {
        configurations.Clear();
        var configs = AssetEditorTools
            .GetAssets<UniBuildCommandsMap>()
            .Select(x => new UniBuildItem(){buildCommands = x});
        
        configurations.AddRange(configs);

        return this;
    }

    private void OnEnable()
    {
        Refresh();
    }
}

#endif