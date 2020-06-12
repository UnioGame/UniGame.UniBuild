namespace UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands.AddressablesCommands
{
    using System.IO;
    using UniCore.Runtime.ProfilerTools;
    using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands;
    using UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Interfaces;
    using UnityEditor;
    using UnityEditor.AddressableAssets.Build;
    using UnityEditor.AddressableAssets.Settings;
    using UnityEditor.Build.Pipeline.Utilities;
    using UnityEngine;

    public enum CleanType
    {
        CleanAll,
        CleanContentBuilders,
        CleanBuildPipelineCache,
    }
    
    [CreateAssetMenu(menuName = "UnityBuild/PreBuildCommands/Addressables Cleanup", fileName = nameof(AddressablesCleanUpCommand))]
    public class AddressablesCleanUpCommand : UnityPreBuildCommand
    {
                
        public const string AddressablesCachePath = "./Library/com.unity.addressables/StreamingAssetsCopy";
        public const string StreamingAddressablesPath = "/aa";
        

        [Tooltip("Clean Addressables Library cache")]
        public bool CleanUpLibraryCache = true;
        
        [Tooltip("Clean Addressables Library cache")]
        public bool CleanUpStreamingCache = true;

        public CleanType CleanType = CleanType.CleanAll;
        
        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            RemoveLibraryCache();
            RemoveStreamingCache();
            
            switch (CleanType) {
                case CleanType.CleanAll:
                    CleanAll();
                    break;
                case CleanType.CleanContentBuilders:
                    OnCleanAddressables(null);
                    break;
                case CleanType.CleanBuildPipelineCache:
                    OnCleanSBP();
                    break;
            }
        }

        public void RemoveLibraryCache()
        {
            if (CleanUpLibraryCache == false) return;
            
            var targetPath = AddressablesCachePath;
            RemoveFolder(targetPath);
        }
        
        public void RemoveStreamingCache()
        {
            if (CleanUpStreamingCache == false) return;
            
            var targetPath = Application.streamingAssetsPath + StreamingAddressablesPath;
            RemoveFolder(targetPath);
        }

        private void RemoveFolder(string path)
        {
            GameLog.Log($"COMMAND: {this.Name} \nRemove Addressables Folder {path}");
            if (Directory.Exists(path)) {
                GameLog.Log($"COMMAND: {this.Name} REMOVE {path}");
                Directory.Delete(path,true);
            }
            else {
                GameLog.Log($"COMMAND: {this.Name}: FAILED Remove addressable folder {path} NOT FOUND");
            }
        }
        
        public void CleanAll()
        {
            OnCleanAddressables(null);
            OnCleanSBP();
        }

        public void OnCleanAddressables(object builder)
        {
            AddressableAssetSettings.CleanPlayerContent(builder as IDataBuilder);
        }

        public void OnCleanSBP()
        {
            BuildCache.PurgeCache(true);
        }
        
    }
}
