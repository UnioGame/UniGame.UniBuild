namespace UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands
{
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

            PlayerSettings.SetManagedStrippingLevel(buildParameters.BuildTargetGroup,_managedStrippingLevel);
        }
        
    }
}
