using System;
using System.IO;
using System.Text;
using UniModules.Editor;
using UnityEngine;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild
{
    public static class BuildLogger
    {
        private const string MessageFormat = "=== UNIBUILD [{0}] : {1}\n";
        private static string BuildDirectory = "Builds".ToAbsoluteProjectPath();
        private static string BuildLogPath = BuildDirectory.CombinePath("last_build_log.log");
        private static string BuildStartMessage = "=== UNIBUILD BUILD REPORT ===\n\n";
        
        private static StringBuilder _buildLog = new StringBuilder();

        public static void Initialize()
        {
            
            _buildLog ??= new StringBuilder(); 
            _buildLog.Clear();
            _buildLog.AppendLine(BuildStartMessage);

            if (!Directory.Exists(BuildDirectory))
                Directory.CreateDirectory(BuildDirectory);

            File.WriteAllText(BuildLogPath,BuildStartMessage);
        }

        public static void Finish() =>  _buildLog?.Clear();
        
        public static void Log(string log)
        {
            Debug.Log(log);
            Append(log);
        }
        
        public static void Append(string log)
        {
            if (string.IsNullOrEmpty(log))
                return;

            var message = string.Format(MessageFormat,DateTime.Now, log);
            _buildLog.AppendLine(message);
            File.AppendAllText(BuildLogPath,message);
        }

        public static void Print() => Debug.Log(_buildLog);
        
    }
}