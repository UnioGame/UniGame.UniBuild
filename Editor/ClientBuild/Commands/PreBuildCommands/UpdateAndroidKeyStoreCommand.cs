namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands {
    using System.Text;
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    [CreateAssetMenu(menuName = "UnityBuild/PreBuildCommands/Update Android KeyStore", fileName = "UpdateAndroidKeyStore")]
    public class UpdateAndroidKeyStoreCommand : UnityPreBuildCommand
    {
        
        //android keys
        public string KeyStorePath      = "-keystorePath";
        public string KeyStorePass      = "-keystorePass";
        public string KeyStoreAlias     = "-keystoreAlias";
        public string KeyStoreAliasPass = "-keystoreAliasPass";

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.FilePath(AbsolutePath = false)]
#endif
        public string _defaultKeyStorePath = string.Empty;
        public string _defaultStorePass = string.Empty;
        public string _defaultStoreAliasPass = string.Empty;
        public string _defaultStoreAlias = string.Empty;
        
        public override void Execute(IUniBuilderConfiguration configuration) 
        {
            if (configuration.BuildParameters.EnvironmentType == BuildEnvironmentType.UnityCloudBuild) {
                Debug.Log("Skipped in UnityCloudBuild environment");
                return;
            }
            
            UpdateAndroidBuildParameters(configuration.Arguments);
        }
        
        public void UpdateAndroidBuildParameters(IArgumentsProvider arguments) {

            //update android key store parameters
            arguments.GetStringValue(KeyStorePath, 
                out var keystore, _defaultKeyStorePath);
            arguments.GetStringValue(KeyStorePass,
                out var keypass, string.IsNullOrEmpty(_defaultStorePass) ? PlayerSettings.Android.keystorePass : _defaultStorePass);

            arguments.GetStringValue(KeyStoreAlias,
                out var alias, string.IsNullOrEmpty(_defaultStoreAlias) ? PlayerSettings.Android.keyaliasName : _defaultStoreAlias);

            arguments.GetStringValue(KeyStoreAliasPass,
                out var aliaspass, string.IsNullOrEmpty(_defaultStoreAliasPass) ? PlayerSettings.Android.keyaliasPass : _defaultStoreAliasPass);

            var stringBuilder = new StringBuilder(300);
            stringBuilder.Append("KEYSTORE : ");
            stringBuilder.Append(keystore);
            stringBuilder.AppendLine();
            
            stringBuilder.Append("KEYSTORE PASS : ");
            stringBuilder.Append(keypass);
            stringBuilder.AppendLine();
            
            stringBuilder.Append("KEYSTORE ALIAS: ");
            stringBuilder.Append(alias);
            stringBuilder.AppendLine();
            
            stringBuilder.Append("KEYSTORE ALIAS PASS: ");
            stringBuilder.Append(aliaspass);
            stringBuilder.AppendLine();
            
            Debug.Log(stringBuilder);

            var isUseKeyStore = Validate(keystore, keypass, alias, aliaspass);
            
            #if UNITY_2019
            
            PlayerSettings.Android.useCustomKeystore = isUseKeyStore;
            
            #endif
            
            if (!isUseKeyStore) {
                return;
            }
            
            PlayerSettings.Android.keystorePass = keypass;
            PlayerSettings.Android.keystoreName = keystore;
            PlayerSettings.Android.keyaliasName = alias;
            PlayerSettings.Android.keyaliasPass = aliaspass;
            
        }

        private bool Validate(string keystore, string pass, string alias,string aliasPass)
        {
            var result = string.IsNullOrEmpty(keystore) ||
                         string.IsNullOrEmpty(keystore) ||
                         string.IsNullOrEmpty(keystore) ||
                         string.IsNullOrEmpty(keystore);
            return !result;
        }
    }
}
