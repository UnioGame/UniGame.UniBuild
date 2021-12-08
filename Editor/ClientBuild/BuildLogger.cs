using System.Text;
using UnityEngine;

namespace UniModules.UniGame.UniBuild.Editor.ClientBuild
{
    public static class BuildLogger
    {
        private const string MessageFormat = "=== UNIBUILD : {0}\n";
        
        private static StringBuilder _buildLog = new StringBuilder();

        public static void Initialize()
        {
            _buildLog ??= new StringBuilder(); 
            _buildLog.Clear();
            _buildLog.AppendLine("=== UNIBUILD BUILD REPORT ===\n\n");
        }

        public static void Finish()
        {
            _buildLog?.Clear();
        }
        
        public static void Log(string log)
        {
            Debug.Log(log);
            Append(log);
        }
        
        public static void Append(string log)
        {
            if (string.IsNullOrEmpty(log))
                return;
            _buildLog.AppendFormat(MessageFormat,log);
        }

        public static void Print()
        {
            Debug.Log(_buildLog);
        }
        
    }
}