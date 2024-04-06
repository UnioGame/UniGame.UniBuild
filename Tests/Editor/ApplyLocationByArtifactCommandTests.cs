namespace UniModules.UniGame.UniBuild.Tests.Editor
{
    using System;
    using NUnit.Framework;
    using UnityEditor;
    using UnityEngine;
    using UniBuild.Editor.ClientBuild.Commands.PreBuildCommands;

    [TestFixture]
    public class ApplyLocationByArtifactCommandTests
    {

        private string artifactNameAab = "Some Island-develop-0.8.10.311.aab";
        private string testBuildLocation = @"D:\Dev\hog_last\HogUnity\Builds";
        
        [Test]
        public void CheckWithBuildTargetFileName()
        {
            //arrange
            var artifact = artifactNameAab;
            var resultString = @$"{testBuildLocation}\Android\SomeIslanddevelop0810311\";
            var command = new ApplyLocationByArtifactCommand()
            {
                option = ArtifactLocationOption.Append,
                useArtifactNameAsFolderPath = true,
                appendBuildTarget = true,
            };

            //action

            var result = command.CreateArtifactLocation(artifact, testBuildLocation,BuildTarget.Android);
            Debug.Log(result);
            Debug.Log(resultString);
            
            //check
            Assert.That(string.Equals(result,resultString, StringComparison.InvariantCultureIgnoreCase));
        }
        
        [Test]
        public void CheckWithBuildFileName()
        {
            //arrange
            var artifact = artifactNameAab;
            var resultString = @$"{testBuildLocation}\SomeIslanddevelop0810311\";
            
            var command = new ApplyLocationByArtifactCommand()
            {
                option = ArtifactLocationOption.Append,
                useArtifactNameAsFolderPath = true,
                appendBuildTarget = false,
            };

            //action

            var result = command.CreateArtifactLocation(artifact, testBuildLocation,BuildTarget.Android);
            Debug.Log(result);
            Debug.Log(resultString);
            
            //check
            Assert.That(string.Equals(result,resultString, StringComparison.InvariantCultureIgnoreCase));
        }
        
        [Test]
        public void CheckBuildLocation()
        {
            //arrange
            var artifact = artifactNameAab;
            var resultString = @$"{testBuildLocation}\";
            var command = new ApplyLocationByArtifactCommand()
            {
                option = ArtifactLocationOption.Append,
                useArtifactNameAsFolderPath = false,
                appendBuildTarget = false,
            };

            //action

            var result = command.CreateArtifactLocation(artifact, testBuildLocation,BuildTarget.Android);
            Debug.Log(result);
            Debug.Log(resultString);
            
            //check
            Assert.That(string.Equals(result,resultString, StringComparison.InvariantCultureIgnoreCase));
        }
        
        
        [Test]
        public void CheckBuildCustomLocation()
        {
            //arrange
            var artifact = artifactNameAab;
            var testLocation = "test_location";
            var resultString = @$"{testLocation}\";
            
            var command = new ApplyLocationByArtifactCommand()
            {
                location = testLocation,
                option = ArtifactLocationOption.Replace,
                useArtifactNameAsFolderPath = false,
                appendBuildTarget = false,
            };

            //action

            var result = command.CreateArtifactLocation(artifact, testBuildLocation,BuildTarget.Android);
            Debug.Log(result);
            Debug.Log(resultString);
            
            //check
            Assert.That(string.Equals(result,resultString,
                StringComparison.InvariantCultureIgnoreCase));
        }
        
        [Test]
        public void CheckBuildCustomLocationAppend()
        {
            //arrange
            var artifact = artifactNameAab;
            var testLocation = "test_location";
            var resultString = @$"{testBuildLocation}\{testLocation}\";
            
            var command = new ApplyLocationByArtifactCommand()
            {
                location = testLocation,
                option = ArtifactLocationOption.Append,
                useArtifactNameAsFolderPath = false,
                appendBuildTarget = false,
            };

            //action

            var result = command.CreateArtifactLocation(artifact, testBuildLocation,BuildTarget.Android);
            
            Debug.Log(result);
            Debug.Log(resultString);
            
            //check
            Assert.That(string.Equals(result,resultString, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
