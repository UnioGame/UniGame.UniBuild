namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild
{
    using System.Text;
    using UnityEditor.Build.Reporting;

    public class BuildReportLogger
    {

        public static string GetBuildReport(BuildReport report) {

            var stringBuilder = new StringBuilder(1000);

            AppendBuildSteps(report,stringBuilder);

            AppendSummary(report, stringBuilder);

            return stringBuilder.ToString();
        }

        public static void AppendSummary(BuildReport report, StringBuilder builder) {

            var summary = report.summary;

            builder.AppendLine("=========BUILD REPORT SUMMARY===========");
            builder.AppendLine($"\tOUTPUT: {summary.outputPath}");
            builder.AppendLine($"\tBUILD RESULT: {summary.result}");
            builder.AppendFormat("\tBuild Target : {0} Group : {1}", summary.platform, summary.platformGroup);
            builder.AppendLine();

            builder.AppendFormat("\tSTART AT: {0}", summary.buildStartedAt);
            builder.AppendLine();
            builder.AppendFormat("\tEND AT: {0}", summary.buildEndedAt);
            builder.AppendLine();
            builder.AppendFormat("\tTOTAL TIME: {0}", summary.totalTime);
            builder.AppendLine();

            builder.AppendLine($"\tTOTAL ERRORS: {summary.totalErrors}");
            builder.AppendLine($"\tTOTAL WARNINGS: {summary.totalWarnings}");
            builder.AppendLine($"\tTOTAL SIZE: {summary.totalSize}");

            builder.AppendLine("=========BUILD SUMMARY FINISHED=========");
        }

        public static void AppendBuildSteps(BuildReport report, StringBuilder builder) {

            builder.AppendLine("=========BUILD STEPS=========");

            var steps = report.steps;
            for (var i = 0; i < steps.Length; i++)
            {
                AppendBuildStep(ref steps[i], builder);
                builder.AppendLine();
            }
            builder.AppendLine("=========BUILD STEPS FINISHED=========");
        }

        public static void AppendBuildStep(ref BuildStep step, StringBuilder builder)
        {
            builder.AppendFormat("\tStep: {0}", step.name);
            builder.AppendFormat("\tDuration (sec): {0}", step.duration.TotalSeconds);
            for (var i = 0; i < step.messages.Length; i++) {
                var message = step.messages[i];
                builder.AppendLine($"\t\tTYPE: {message.type}");
                builder.AppendLine(message.content);
            }
        }

    }
}
