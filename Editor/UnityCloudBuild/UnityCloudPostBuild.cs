using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace UniGame
{
    public class UnityCloudPostBuild : IPostprocessBuildWithReport
    {
        public static string BuildFileKey = nameof(BuildFileKey);
        private static List<string> buildFiles = new List<string>();

        public static List<string> OutputFiles
        {
            get
            {
                if (buildFiles != null)
                    return buildFiles;
                var value = EditorPrefs.HasKey(BuildFileKey) ?
                    EditorPrefs.GetString(BuildFileKey) :
                    string.Empty;
                buildFiles = string.IsNullOrEmpty(value)
                    ? new List<string>()
                    : JsonConvert.DeserializeObject<List<string>>(value);

                return buildFiles;
            }
            set => buildFiles = value;
        }

        public int callbackOrder { get; } = 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            Debug.Log($"===== UNIBUILD{nameof(UnityCloudPostBuild)} : {report}");

            var files = report.GetFiles().Select(x => x.path).ToList();

            var buildResults = JsonConvert.SerializeObject(files);
            
            EditorPrefs.SetString(BuildFileKey,buildResults);

            OutputFiles = files;
        }
    }
}