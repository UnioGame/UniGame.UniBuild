//using NUnit.Framework;
//
//namespace UniGreenModules.UnityBuild.Tests.Editor
//{
//    using UnityBuild.Editor.ClientBuild;
//    using UnityBuild.Editor.ClientBuild.Interfaces;
//    using UnityEditor;
//
//    [TestFixture]
//    public class BuildParametersTests
//    {
//        [Test]
//        public void ApplyBuildOptionsOverride()
//        {
//            //arrange
//                        
//            var options1 = BuildOptions.None;
//            options1 |= BuildOptions.Development;
//            options1 |= BuildOptions.ConnectWithProfiler;
//            
//            var options2 = BuildOptions.None;
//            options2 |= BuildOptions.Reserved1;
//            options2 |= BuildOptions.StrictMode;
//
//            var buildTarget = BuildTarget.Android;
//            var buildTargetGroup = BuildTargetGroup.Android;
//            var arguments = NSubstitute.Substitute.For<IArgumentsProvider>();
//
//            var buildParameters = new BuildParameters(buildTarget,buildTargetGroup,arguments);
//
//            //action
//            buildParameters.SetBuildOptions(options2,false);
//            buildParameters.SetBuildOptions(options1,true);
//            
//            //assert
//            Assert.That(buildParameters.BuildOptions == options1);
//            
//       }
//    
//        
//        [Test]
//        public void ApplyBuildOptionsConcat()
//        {
//            //arrange
//                        
//            var options1 = BuildOptions.None;
//            options1 |= BuildOptions.Development;
//            options1 |= BuildOptions.ConnectWithProfiler;
//            
//            var options2 = BuildOptions.None;
//            options2 |= BuildOptions.Reserved1;
//            options2 |= BuildOptions.StrictMode;
//
//            var resultOptions = options1 | options2;
//            
//            var buildTarget      = BuildTarget.Android;
//            var buildTargetGroup = BuildTargetGroup.Android;
//            var arguments        = NSubstitute.Substitute.For<IArgumentsProvider>();
//
//            var buildParameters = new BuildParameters(buildTarget,buildTargetGroup,arguments);
//
//            //action
//            buildParameters.SetBuildOptions(options2,false);
//            buildParameters.SetBuildOptions(options1,false);
//            
//            //assert
//            Assert.That(buildParameters.BuildOptions == resultOptions);
//            
//        }
//    }
//}
