namespace UniGame.UniBuild.Editor.Commands.PreBuildCommands
{
    using System.IO;
    using global::UniGame.Core.Runtime.Extension;
    using UnityEditor;
    using System;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using UniModules;

    [Serializable]
    public class ApplyLocationByArtifactCommand : SerializableBuildCommand
    {

        public ArtifactLocationOption option = ArtifactLocationOption.Append;

        public bool useArtifactNameAsFolderPath = true;

        public string location;

        public bool appendBuildTarget = true;
        
        public override void Execute(IUniBuilderConfiguration configuration)
        {
            var buildParameters = configuration.BuildParameters;
            var resultLocation = buildParameters.outputFolder;
            var artifact = buildParameters.outputFile;
            var buildTarget = buildParameters.buildTarget;
            
            resultLocation =CreateArtifactLocation(artifact,resultLocation,buildTarget);
            buildParameters.outputFolder = resultLocation;
        }

        public string CreateArtifactLocation(string artifactName,string sourceLocation,BuildTarget buildTarget)
        {
            var artifact = Path
                .GetFileNameWithoutExtension(artifactName)
                .RemoveSpecialAndDotsCharacters()
                .RemoveWhiteSpaces();
            
            var locationPath = location
                .RemoveSpecialAndDotsCharacters()
                .RemoveWhiteSpaces();

            locationPath = option == ArtifactLocationOption.Replace
                ? locationPath
                : sourceLocation.CombinePath(locationPath);
            
            locationPath = appendBuildTarget ? locationPath.CombinePath(buildTarget.ToString()) : locationPath;
            locationPath = useArtifactNameAsFolderPath ? locationPath.CombinePath(artifact) : locationPath;
            return locationPath.CombinePath(string.Empty);
        }
    }

    public enum ArtifactLocationOption
    {
        Append,
        Replace
    }
}
