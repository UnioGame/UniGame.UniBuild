namespace UniGreenModules.UniGame.UnityBuild.Editor.UnityCloudBuild
{
    using System.Text;

    internal class CloudBuildArgs
    {
        public CloudBuildArgs(int buildNumber, string bundleId, string projectId, string scmCommitId, string scmBranch, string cloudBuildTargetName)
        {
            BuildNumber = buildNumber;
            BundleId    = bundleId;
            ProjectId   = projectId;
            ScmCommitId = scmCommitId;
            ScmBranch = scmBranch;
            CloudBuildTargetName = cloudBuildTargetName;
        }

        /// <summary>
        /// The Unity Cloud Build “build number” corresponding to this build.
        /// </summary>
        public int BuildNumber { get; }

        /// <summary>
        /// The bundleIdentifier configured in Unity Cloud Build (iOS and Android only).
        /// </summary>
        public string BundleId { get; }

        /// <summary>
        /// The Unity project identifier.
        /// </summary>
        public string ProjectId { get; }

        /// <summary>
        /// The commit or changelist that was built.
        /// </summary>
        public string ScmCommitId { get; }

        /// <summary>
        /// The name of the branch that was built.
        /// </summary>
        public string ScmBranch { get; }

        /// <summary>
        /// The name of the build target that was built (pipeline identifier).
        /// </summary>
        public string CloudBuildTargetName { get; }

        public override string ToString()
        {

            var builder = new StringBuilder(200);
            
            builder.AppendLine();
            builder.AppendLine("=======CloudBuildArgs START=========");
            builder.AppendFormat("ProjectId {0}",ProjectId);
            builder.AppendLine();
            builder.AppendFormat("BundleId {0}",BundleId);
            builder.AppendLine();
            builder.AppendFormat("ScmCommitId {0}",ScmCommitId);
            builder.AppendLine();
            builder.AppendFormat("ScmBranch {0}",ScmBranch);
            builder.AppendLine();
            builder.AppendFormat("CloudBuildTargetName {0}",CloudBuildTargetName);
            builder.AppendLine();
            builder.AppendFormat("BuildNumber {0}",BuildNumber);
            builder.AppendLine();
            builder.AppendLine("=======CloudBuildArgs END=========");
            builder.AppendLine();
            
            return builder.ToString();
        }
    }
}