namespace UniModules.UniGame.UniBuild
{
    using System;
    using Sirenix.OdinInspector;
    using UnityEngine;

    [Serializable]
    [HideLabel]
    public class BuildArgumentValue
    {
        [VerticalGroup]
        public string Value;
        [VerticalGroup]
        [Tooltip("override outer arguments with this value")]
        public bool Override;
    }
}