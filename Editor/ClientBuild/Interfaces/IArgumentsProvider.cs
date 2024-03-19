namespace UniGame.UniBuild.Editor.ClientBuild.Interfaces {
    using System;
    using System.Collections.Generic;

    public interface IArgumentsProvider
    {
        List<string> SourceArguments { get; }
        string EvaluateValue(string expression);
        IReadOnlyDictionary<string, string> Arguments { get; }
        
        public void SetArgument(string key, string value);
        
        string SetValue(string key, string value);
        bool GetIntValue(string name, out int result, int defaultValue = 0);
        bool GetBoolValue(string name, out bool result, bool defaultValue = false);
        bool Contains(string name);

        bool GetEnumValue<TEnum>(string parameterName,out TEnum result)
            where TEnum : struct;
        
        bool GetEnumValue<TEnum>(string parameterName,Type enumType, out TEnum result)
            where TEnum : struct;

        bool GetStringValue(string name, out string result,string defaultValue = "");
    }
}