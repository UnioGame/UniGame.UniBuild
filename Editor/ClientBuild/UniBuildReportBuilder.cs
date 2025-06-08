namespace UniGame.UniBuild.Editor
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using UnityEngine;

    [Serializable]
    public class UniBuildReportBuilder
    {
        private static readonly string Eol = Environment.NewLine;
        private StringBuilder _reportBuilder = new();
        
        public void ApplyReport(BuildReportData reportData)
        {
            _reportBuilder.Clear();

            var report = reportData.report;
            if (report == null) return;
            
            PrintSummaryInfo(reportData,_reportBuilder);
            PrintStrippingInfo(reportData,_reportBuilder);
            PrintStepsInfo(reportData,_reportBuilder);
            PrintPackedInfo(reportData,_reportBuilder);
            
            var result = _reportBuilder.ToString();
            
            PrintToDebugLog(reportData,result);
            PrintToFile(reportData,result);
        }

        public void PrintToDebugLog(BuildReportData reportData, string result)
        {
            if (!reportData.writeLog) return;
            Debug.Log(result);
        }
        
        public void PrintToFile(BuildReportData reportData,string result)
        {
            if (!reportData.writeToFile) return;
            
            var report = reportData.report;
            var summary = report.summary;
            var outputFile = summary.outputPath;
            
            if (string.IsNullOrEmpty(outputFile)) return;
            var outputPath = Path.GetDirectoryName(outputFile);
            if(!Directory.Exists(outputPath))
                outputPath = Path.GetDirectoryName(outputPath);
            if (!Directory.Exists(outputPath))
            {
                Debug.LogError($"Output directory not found for writing build report : {outputPath}");
                return;
            }
            
            var dateTime = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss");
            var logPath = Path.Combine(outputPath, $"build_report_{dateTime}.log");
            
            Debug.Log("Write build report to file : " + logPath);
            
            File.WriteAllText(logPath,result);
        }
        
        public void PrintSummaryInfo(BuildReportData reportData,StringBuilder stringBuilder)
        {
            var summary = reportData.report.summary;
            
            var summaryText = $"{Eol}" +
                              $"###########################{Eol}" +
                              $"#	  Build results	  #{Eol}" +
                              $"###########################{Eol}" +
                              $"{Eol}" +
                              $"Output: {summary.outputPath}{Eol}" +
                              $"Duration: {summary.totalTime.ToString()}{Eol}" +
                              $"Warnings: {summary.totalWarnings.ToString()}{Eol}" +
                              $"Errors: {summary.totalErrors.ToString()}{Eol}" +
                              $"Size: {summary.totalSize.ToString()} bytes{Eol}" +
                              $"{Eol}";
            
            stringBuilder.Append(summaryText);
        }
        
        public void PrintPackedInfo(BuildReportData reportData,StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"===== Packed Assets Info=====");
            
            var packedData = reportData.report.packedAssets;
            if(packedData == null) return;
            
            var index = 0;
            foreach (var packedItem in packedData)
            {
                stringBuilder.AppendLine($"{index} Packed Asset: {packedItem.name} | {packedItem.shortPath}");
                stringBuilder.AppendLine("==== Packed Items:");
                
                var infos = packedItem
                    .contents
                    .OrderByDescending(x => x.packedSize);

                foreach (var assetInfo in infos)
                {
                    stringBuilder.AppendLine();
                    stringBuilder.AppendLine($"\tPath: {assetInfo.sourceAssetPath}");
                    stringBuilder.AppendLine($"\tAsset Type: {assetInfo.type?.Name} GUID: {assetInfo.sourceAssetGUID}");
                    stringBuilder.AppendLine($"\tSize: {assetInfo.packedSize}");
                    stringBuilder.AppendLine();
                }
                
                stringBuilder.AppendLine();
                index++;
            }
            
            stringBuilder.AppendLine($"===== Packed Assets Info End=====");
            stringBuilder.AppendLine();
        }
        
        public void PrintStepsInfo(BuildReportData reportData,StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"===== Build Steps Info =====");
            
            var steps = reportData.report.steps;
            if(steps == null) return;
            
            var index = 0;
            
            foreach (var buildStep in steps)
            {
                stringBuilder.AppendLine($"[{index}] STEP {buildStep.name} | Duration {buildStep.duration}");
                stringBuilder.AppendLine($"\t Depth {buildStep.depth} Message {buildStep.messages}");
                index++;
            }
            
            stringBuilder.AppendLine($"===== Build Steps Info End =====");
            stringBuilder.AppendLine();
        }
        
        public void PrintStrippingInfo(BuildReportData reportData,StringBuilder stringBuilder)
        {
            stringBuilder.AppendLine($"===== Stripping Info=====");
            
            var stripingInfo = reportData.report.strippingInfo;
            if(stripingInfo == null) return;
            
            var included = stripingInfo.includedModules;
            var index = 0;
            
            stringBuilder.AppendLine($"Included Modules:");
            
            foreach (var includedValue in included)
            {
                var reason = stripingInfo.GetReasonsForIncluding(includedValue);
                stringBuilder.AppendLine($"[{index}] : {includedValue}");
                
                foreach (var reasonItem in reason)
                {
                    stringBuilder.AppendLine($"\t{reasonItem}");
                }
                
                index++;
            }
            
            stringBuilder.AppendLine($"===== Stripping Info End=====");
            stringBuilder.AppendLine();
        }
    }
}