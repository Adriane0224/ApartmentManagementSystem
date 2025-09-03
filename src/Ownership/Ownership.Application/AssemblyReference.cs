using System.Reflection;

namespace Ownership.Application
{
    public class AssemblyReference
    {
        public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
    }
}
