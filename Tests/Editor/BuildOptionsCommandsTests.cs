//using NSubstitute;
//using NUnit.Framework;
//using UniGreenModules.UnityBuild.Editor.ClientBuild;
//using UniGreenModules.UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands;
//using UniGreenModules.UnityBuild.Editor.ClientBuild.Interfaces;
//using UnityEditor;
//using UnityEngine;
//
//[TestFixture]
//public class BuildOptionsCommandsTests : MonoBehaviour
//{
//
//    [Test]
//    public void BuildOptionsParsingTest()
//    {
//        //arrange
//        var optionsCommand = new BuildOptionsCommand();
//        var parameterBuildOptions = BuildOptions.None;
//        
//        var resultBuildOptions = BuildOptions.None;
//        resultBuildOptions |= BuildOptions.Development;
//        resultBuildOptions |= BuildOptions.ConnectWithProfiler;
//        
//        var arguments = NSubstitute.Substitute.For<IArgumentsProvider>();
//        arguments.Contains("-development").Returns(true);
//        arguments.Contains("-connectwithprofiler").Returns(true);
//        
//        var parameters = NSubstitute.Substitute.For<IBuildParameters>();
//        parameters.SetBuildOptions(Arg.Do<BuildOptions>(x => parameterBuildOptions = x), Arg.Any<bool>());
//        
//        var configuration = NSubstitute.Substitute.For<IUniBuilderConfiguration>();
//        configuration.Arguments.Returns(arguments);
//        configuration.BuildParameters.Returns(parameters);
//        
//        //act
//
//        optionsCommand.Execute(configuration);
//
//        //assert
//        Assert.That(resultBuildOptions == parameterBuildOptions);
//        
//    }
//   
//    
//    
//}
