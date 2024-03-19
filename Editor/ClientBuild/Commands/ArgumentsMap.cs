namespace UniModules.UniGame.UniBuild
{
    using System;
    using Core.Runtime.DataStructure;

    [Serializable]
    public class ArgumentsMap
    {
        public bool isEnable = true;
        public SerializableDictionary<string, BuildArgumentValue> arguments = new();
    }
}