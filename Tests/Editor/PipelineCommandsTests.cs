using NUnit.Framework;
using UniGame.UniBuild.Editor;

namespace UniModules.UniGame.UniBuild.Tests.Editor
{
    public class PipelineTests 
    {
        [Test]
        public void GetCloudMethodsTest()
        {
            //init
            var generator = new CloudBuildMethodsGenerator();

            //action
            var methods = generator.LoadMethodsTemplate();
            
            Assert.That(string.IsNullOrEmpty(methods) == false);
        }
    }
}
