namespace UniGreenModules.UniGame.UnityBuild.Editor.ClientBuild.Parsers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;

    public class EnumArgumentParser<TValue> 
        where TValue : struct
    {
        private static List<string> buildOptionsNames = Enum.GetNames(typeof(TValue)).
            Select(x => x.Insert(0, "-").ToLower()).
            ToList();
        
        private static List<TValue> buildOptionsValues = Enum.GetValues(typeof(TValue)).
            OfType<TValue>().
            ToList();

        public List<TValue> Parse(IArgumentsProvider arguments)
        {
            var result  = new List<TValue>();

            for (var i = 0; i < buildOptionsValues.Count; i++) {
                var optionName = buildOptionsNames[i];
                if (arguments.Contains(optionName))
                {
                    result.Add(buildOptionsValues[i]);
                }
            }

            return result;
        }
    }
}
