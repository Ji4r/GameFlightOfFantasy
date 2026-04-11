using System;
using UnityEngine;

namespace DiplomGames
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class InjectAttribute : Attribute
    {
        public string Tag { get; set; }

        public InjectAttribute() { }

        public InjectAttribute(string tag)
        {
            Tag = tag;
        }
    }
}
