namespace UniModules.UniGame.UniBuild
{
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using global::UniGame.UniBuild.Editor;
    using global::UniGame.UniBuild.Editor.Commands.PreBuildCommands;
    using global::UniGame.UniBuild.Editor.Interfaces;


    [Serializable]
    public class ApplyBuildArgumentsCommand : SerializableBuildCommand
    {        
        public bool logArguments = true;

        public ArgumentsMap argumentsMap = new ArgumentsMap();

        public override void Execute(IUniBuilderConfiguration buildParameters)
        {
            var arguments = buildParameters.Arguments;
            if (arguments == null) return;

            foreach (var argPair in argumentsMap.arguments)
            {
                if(logArguments)
                    BuildLogger.Log($"\n\t\tBUILD ARG: {argPair.Key} : {argPair.Value}");
                arguments.SetValue(argPair.Key, argPair.Value.Value);
            }
        }
    }
}