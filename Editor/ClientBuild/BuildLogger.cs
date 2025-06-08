using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UniModules.Editor;
using UniGame.Runtime.DateTime;
using UnityEngine;

namespace UniGame.UniBuild.Editor
{
    using UniModules;

    public static class BuildLogger
    {
        private const string MessageFormat = "=== UNIBUILD [{0}] : {1} {2}\n";
        private const string DurationFormat = "| DURATION [{0} sec]";
        private static string BuildDirectory = "Builds".ToAbsoluteProjectPath();
        private static string BuildLogPath = BuildDirectory.CombinePath("last_build_log.log");
        private static string BuildStartMessage = "=== UNIBUILD BUILD REPORT ===\n\n";
        
        private static Dictionary<string,int> _timeLog = new Dictionary<string, int>();
        private static StringBuilder _buildLog = new StringBuilder();

        public static void Initialize()
        {
            _timeLog = new Dictionary<string, int>();
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
            Log(log,string.Empty,false);
        }
        
        public static string LogWithTimeTrack(string log)
        {
            Debug.Log(log);
            var id = Guid.NewGuid().ToString();
            Append(log,id,true);
            return id;
        }
        
        public static void Log(string log,string trackId,bool resetTime = false)
        {
            Debug.Log(log);
            Append(log,trackId,resetTime);
        }
        
        public static void Append(string log,string trackId,bool resetDuration)
        {
            if (string.IsNullOrEmpty(log))
                return;

            if (resetDuration) _timeLog.Remove(trackId);
            
            var now = DateTime.Now;
            var nowUnix = (int)now.ToUnixTimestamp();
            
            if (!_timeLog.TryGetValue(trackId, out var logTime))
            {
                if (!string.IsNullOrEmpty(trackId))
                {
                    logTime = nowUnix;
                    _timeLog[trackId] = logTime;
                }
            }

            var durationTime = nowUnix - logTime;
            var duration = durationTime > 0 ? string.Format(DurationFormat,durationTime) : string.Empty;
            var message = string.Format(MessageFormat,now,log,duration);
            
            _buildLog.AppendLine(message);
            
            File.AppendAllText(BuildLogPath,message);
        }

        public static void Print() => Debug.Log(_buildLog);
        
    }
}