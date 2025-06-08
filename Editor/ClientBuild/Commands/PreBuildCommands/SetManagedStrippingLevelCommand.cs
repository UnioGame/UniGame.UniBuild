namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands
{
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// update current project version
    /// </summary>
    [SerializeField]
    public class SetManagedStrippingLevelCommand : UnitySerializablePreBuildCommand
    {
        [SerializeField]
        private ManagedStrippingLevel _managedStrippingLevel = ManagedStrippingLevel.Disabled;

        public override void Execute(IUniBuilderConfiguration configuration)
        {
            var buildParameters = configuration.BuildParameters;

            PlayerSettings.SetManagedStrippingLevel(buildParameters.buildTargetGroup,_managedStrippingLevel);
        }
        
    }
}
