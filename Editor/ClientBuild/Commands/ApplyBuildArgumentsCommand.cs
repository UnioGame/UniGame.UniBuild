namespace UniModules.UniGame.UniBuild
{
    using System;
    using UniModules.UniGame.Core.Runtime.DataStructure;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;
    using UniModules.UniGame.UniBuild.Editor.ClientBuild.Interfaces;

    
    [Serializable]
    public class ApplyBuildArgumentsCommand : SerializableBuildCommand
    {
        
        public ArgumentsMap argumentsMap = new ArgumentsMap();
        
        public bool logArguments = true;
        
        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            var arguments = buildParameters.Arguments;
            if (arguments == null) return;

            foreach (var argPair in argumentsMap)
            {
                if(logArguments)
                    BuildLogger.Log($"\n\t\tBUILD ARG: {argPair.Key} : {argPair.Value}");
                arguments.SetValue(argPair.Key, argPair.Value);
            }
        }
    }
    
    
    [Serializable]
    public class ArgumentsMap : SerializableDictionary<string, string>
    {
        
    }
}