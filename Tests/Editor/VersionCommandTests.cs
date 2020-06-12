namespace UniGreenModules.UniGame.UnityBuild.Tests.Editor
{
    using NUnit.Framework;
    using UnityBuild.Editor.ClientBuild.Commands.PreBuildCommands.UpdateVersionCommand;
    using UnityEditor;

    [TestFixture]
    public class VersionCommandTests
    {
        [Test]
        [TestCase(BuildTarget.Android, null, ExpectedResult           = "1.0.2.100")]
        [TestCase(BuildTarget.Android, "master", ExpectedResult       = "1.0.2.100")]
        [TestCase(BuildTarget.Android, "develop", ExpectedResult      = "1.0.2.100 develop")]
        [TestCase(BuildTarget.Android, "feature/name", ExpectedResult = "1.0.2.100 name")]
        [TestCase(BuildTarget.Android, "custom/name", ExpectedResult  = "1.0.2.100 custom/name")]
        [TestCase(BuildTarget.iOS, null, ExpectedResult               = "1.0.100")]
        [TestCase(BuildTarget.iOS, "master", ExpectedResult           = "1.0.100")]
        [TestCase(BuildTarget.iOS, "develop", ExpectedResult          = "1.0.100")]
        [TestCase(BuildTarget.iOS, "feature/name", ExpectedResult     = "1.0.100")]
        [TestCase(BuildTarget.iOS, "custom/name", ExpectedResult      = "1.0.100")]
        public string GetVersionTest(BuildTarget target, string branch)
        {
            //arrange

            var buildHandler  = new BuildVersionProvider();
            var buildNumber   = 100;
            var bundleVersion = "1.0.2";

            //act

            var version = buildHandler.GetBuildVersion(target, bundleVersion, buildNumber, branch);

            //assert
            
            return version;
        }
    }
}