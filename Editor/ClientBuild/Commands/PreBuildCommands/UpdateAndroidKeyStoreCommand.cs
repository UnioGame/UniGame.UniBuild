namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
    using System;
    using System.Text;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

#if ODIN_INSPECTOR
     using Sirenix.OdinInspector;
#endif

#if TRI_INSPECTOR
    using TriInspector;
#endif
    
    [Serializable]
    public class UpdateAndroidKeyStoreCommand : UnitySerializablePreBuildCommand
    {
        [Space]
        public bool useDebugKeyStore = false;

        [Space]
#if ODIN_INSPECTOR
        [VerticalGroup()]
#endif
        //android keys
        public string KeyStorePath      = "-keystorePath";
#if ODIN_INSPECTOR
        [VerticalGroup()]
#endif
        public string KeyStorePass      = "-keystorePass";
#if ODIN_INSPECTOR
        [VerticalGroup()]
#endif
        public string KeyStoreAlias     = "-keystoreAlias";
#if ODIN_INSPECTOR
        [VerticalGroup()]
#endif
        public string KeyStoreAliasPass = "-keystoreAliasPass";

#if ODIN_INSPECTOR
        [Sirenix.OdinInspector.FilePath(AbsolutePath = false)]
#endif
        public string _defaultKeyStorePath = string.Empty;
        public string _defaultStorePass      = string.Empty;
        public string _defaultStoreAliasPass = string.Empty;
        public string _defaultStoreAlias     = string.Empty;

#if  ODIN_INSPECTOR || TRI_INSPECTOR
        [Button]
#endif
        public void Execute()
        {
            ApplyKeyStoreData(_defaultKeyStorePath, _defaultStorePass, _defaultStoreAlias, _defaultStoreAliasPass);
        }

        public override void Execute(IUniBuilderConfiguration configuration)
        {
#if UNITY_CLOUD_BUILD
            return;
#endif
            if (configuration.BuildParameters.environmentType == BuildEnvironmentType.UnityCloudBuild) {
                Debug.Log("Skipped in UnityCloudBuild environment");
                return;
            }

            UpdateAndroidBuildParameters(configuration.Arguments);
        }

        public void UpdateAndroidBuildParameters(IArgumentsProvider arguments)
        {
            
            
            //update android key store parameters
            arguments.GetStringValue(KeyStorePath, out var keystore, _defaultKeyStorePath);
            arguments.GetStringValue(KeyStorePass, out var keypass, string.IsNullOrEmpty(_defaultStorePass) ? PlayerSettings.Android.keystorePass : _defaultStorePass);
            arguments.GetStringValue(KeyStoreAlias, out var alias, string.IsNullOrEmpty(_defaultStoreAlias) ? PlayerSettings.Android.keyaliasName : _defaultStoreAlias);
            arguments.GetStringValue(KeyStoreAliasPass, out var aliaspass, string.IsNullOrEmpty(_defaultStoreAliasPass) ? PlayerSettings.Android.keyaliasPass : _defaultStoreAliasPass);

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

            ApplyKeyStoreData(keystore, keypass, alias, aliaspass);
        }

        private void ApplyKeyStoreData(string keystore, string keypass, string alias, string aliaspass)
        {
            var isUseKeyStore = Validate(keystore, keypass, alias, aliaspass);

            if (!isUseKeyStore)
            {
                PlayerSettings.Android.useCustomKeystore = true;
                return;
            }
            
            PlayerSettings.Android.useCustomKeystore = !useDebugKeyStore;
            PlayerSettings.Android.keystorePass = keypass;
            PlayerSettings.Android.keystoreName = keystore;
            PlayerSettings.Android.keyaliasName = alias;
            PlayerSettings.Android.keyaliasPass = aliaspass;
        }

        private bool Validate(string keystore, string pass, string alias, string aliasPass)
        {
            var result = string.IsNullOrEmpty(keystore) ||
                         string.IsNullOrEmpty(pass) ||
                         string.IsNullOrEmpty(alias) ||
                         string.IsNullOrEmpty(aliasPass);
            return !result;
        }
    }
}