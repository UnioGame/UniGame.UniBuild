namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild
{
    using System.Text;
    using UnityEditor.Build.Reporting;

    public static class UnityBuildExtensions 
    {

        private static StringBuilder stringBuilder = new StringBuilder(1000);

        public static string ReportMessage(this BuildReport report) {

            return BuildReportLogger.GetBuildReport(report);

        }

        public static string GetSummaryMessage(this BuildReport report) {

            BuildReportLogger.AppendSummary(report,stringBuilder);
            var message = stringBuilder.ToString();
            stringBuilder.Clear();
            return message;

        }

        public static string GetStepsMessage(this BuildReport report)
        {

            BuildReportLogger.AppendBuildSteps(report, stringBuilder);
            var message = stringBuilder.ToString();
            stringBuilder.Clear();
            return message;

        }

    }
}
