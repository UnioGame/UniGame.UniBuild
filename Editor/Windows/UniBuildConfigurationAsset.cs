#if  ODIN_INSPECTOR || TRI_INSPECTOR

using System.Collections.Generic;
using System.Linq;
using UniModules.Editor;
using UniGame.UniBuild.Editor;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
using TriInspector;
#endif

[CreateAssetMenu(menuName = "UniBuild/UniBuild ConfigurationAsset", fileName ="UniBuildConfigurationAsset")]
public class UniBuildConfigurationAsset : ScriptableObject
{
    #region public properties
        
#if ODIN_INSPECTOR
    [Searchable]
    [ListDrawerSettings(Expanded = true)]
#endif
#if TRI_INSPECTOR || ODIN_INSPECTOR
    [InlineProperty]
#endif
    public List<UniBuildItem> configurations = new List<UniBuildItem>();

    #endregion
    
#if TRI_INSPECTOR || ODIN_INSPECTOR
    [Button]
#endif
#if ODIN_INSPECTOR
    [OnInspectorInit]
#endif
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