namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild 
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Interfaces;

    public class ArgumentsProvider : IArgumentsProvider 
    {
        
        private const string SeparatorValue = ":";
    
        private Dictionary<string, string> arguments;
    
        public ArgumentsProvider(string[] arguments)
        {
            this.SourceArguments = new List<string>();
            this.SourceArguments.AddRange(arguments);

            this.arguments = ParseInputArgumets(arguments);
        }
    
        public List<string> SourceArguments { get; private set; }

        public IReadOnlyDictionary<string, string> Arguments => this.arguments;

        public bool GetIntValue(string name, out int result, int defaultValue = 0)
        {

            if (this.GetStringValue(name, out var value))
            {
                return int.TryParse(value, out result);
            }

            result = defaultValue;
            return false;
        }

        public bool GetBoolValue(string name, out bool result, bool defaultValue = false)
        {
            if (this.GetStringValue(name, out var value))
            {
                return bool.TryParse(value, out result);
            }
        
            result = defaultValue;
            return false;
        }

        public bool Contains(string name)
        {
            var contain = this.Arguments.ContainsKey(name);
            return contain;
        }
    
        public bool GetEnumValue<TEnum>(string parameterName,Type enumType, out TEnum result)
            where TEnum : struct
        {

            if (this.GetStringValue(parameterName, out var value))
            {
                return Enum.TryParse(value, out result);
            }
        
            result = default(TEnum);
            return false;
        }
    
        public bool GetStringValue(string name, out string result,string defaultValue = "")
        {
            if (this.Arguments.ContainsKey(name))
            {
                var value = this.Arguments[name];
                value = value.TrimStart();
            
                result = value;
                if (!string.IsNullOrEmpty(result))
                    return true;
            }

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
            
                resultArguments[key] = value;
                resultArguments[key.ToLower()] = value;
            }

            return resultArguments;
            
        }
    }
}
