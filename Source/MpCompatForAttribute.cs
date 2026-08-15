using System;

namespace AstroMultiplayerCompability
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MpCompatForAttribute : Attribute
    {
        public string PackageId { get; }

        public MpCompatForAttribute(string packageId)
        {
            PackageId = packageId.ToLower();
        }
    }
}
