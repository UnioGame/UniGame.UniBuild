using System.Text.RegularExpressions;

namespace UniGame.UniBuild.Editor 
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using global::UniGame.UniBuild.Editor.ClientBuild.Interfaces;
    using Interfaces;

    public class ArgumentsProvider : IArgumentsProvider 
    {
        
        private const string SeparatorValue = ":";
        private const char ArgumentKey = '$';
        private const string ArgumentRegExprPattern = @"(\$[\w,\d]+)";

        private Regex argumentRefExpr = new Regex(ArgumentRegExprPattern,
            RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        
        private Dictionary<string, string> arguments;
    
        public ArgumentsProvider(string[] arguments)
        {
            SourceArguments = new List<string>();
            SourceArguments.AddRange(arguments);

            this.arguments = ParseInputArgumets(arguments);
        }
    
        public List<string> SourceArguments { get; private set; }

        public IReadOnlyDictionary<string, string> Arguments => arguments;

        public string EvaluateValue(string expression)
        {
            var matches = argumentRefExpr.Matches(expression);
            var resultExpression = expression;
            
            foreach (var match in matches)
            {
                var key = match.ToString();
                GetStringValue(key, out var value, string.Empty);
                resultExpression = resultExpression.Replace(key, value);
            }

            return resultExpression;
        }

        public void SetArgument(string key, string value)
        {
            arguments[key] = value;
        }

        public string SetValue(string key, string value)
        {
            var argument = key.TrimStart(ArgumentKey);
            arguments[argument] = value;
            return value;
        }
        
        public bool GetIntValue(string name, out int result, int defaultValue = 0)
        {
            if (GetStringValue(name, out var value))
                return int.TryParse(value, out result);
            
            result = defaultValue;
            return false;
        }

        public bool GetBoolValue(string name, out bool result, bool defaultValue = false)
        {
            if (GetStringValue(name, out var value))
            {
                return bool.TryParse(value, out result);
            }
        
            result = defaultValue;
            return false;
        }

        public bool Contains(string name)
        {
            var contain = Arguments.ContainsKey(name);
            return contain;
        }
    
        public bool GetEnumValue<TEnum>(string parameterName,out TEnum result)
            where TEnum : struct
        {
            if (GetStringValue(parameterName, out var value))
                return Enum.TryParse(value, out result);
            
            result = default(TEnum);
            return false;
        }
        
        public bool GetEnumValue<TEnum>(string parameterName,Type enumType, out TEnum result)
            where TEnum : struct
        {
            return GetEnumValue(parameterName, out result);
        }
    
        public bool GetStringValue(string name, out string result,string defaultValue = "")
        {
            if (GetArgument(name,out result))
                return true;

            var argumentName = name.TrimStart(ArgumentKey);
            if (GetArgument(argumentName,out result))
                return true;

            result = defaultValue;
            return false;
        }

        public override string ToString() {
            
            var stringBuilder = new StringBuilder(200);
            stringBuilder.Append("ALL BUILD ARGUMENTS : ");

            foreach (var sourceArgument in SourceArguments) {
                stringBuilder.Append(" ");
                stringBuilder.Append(sourceArgument);
                stringBuilder.AppendLine();
            }
            
            stringBuilder.Append("KEYS :");
            foreach (var argument in Arguments) {
                stringBuilder.Append("KEY : ");
                stringBuilder.Append(argument.Key);
                stringBuilder.Append(" : ");
                stringBuilder.Append(argument.Value);
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        private bool GetArgument(string name, out string result)
        {
            result = string.Empty;
            
            if (!Arguments.ContainsKey(name)) return false;
            
            var value = Arguments[name];
            value = value.TrimStart();
            result = value;
            
            return true;
        }
        
        private Dictionary<string,string> ParseInputArgumets(string[] args) {
        
            var resultArguments = new Dictionary<string,string>();
            
            for (var i = 0; i < args.Length; i++)
            {
                var argument = args[i];
                var key      = argument;
                var value    = string.Empty;
            
                var separatorIndex = argument.IndexOf(SeparatorValue, StringComparison.OrdinalIgnoreCase);
            
                if (separatorIndex > 0)
                {
                    var lenght = SeparatorValue.Length;
                    key = argument.Substring(0, separatorIndex);
                    value = argument.Substring(separatorIndex + lenght, 
                        argument.Length - separatorIndex - lenght);
                }
                else
                {
                    var next = i + 1;
                    if(next < args.Length) value = args[next];
                }
            
                resultArguments[key] = value;
                resultArguments[key.ToLower()] = value;
            }

            return resultArguments;
            
        }
    }
}
