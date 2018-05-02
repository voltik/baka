using Mono.Cecil;

namespace CecilTest
{
    public class MethodDesc
    {
        public string Signature { get; set; }
        public bool IsPublic { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsGetter { get; set; }
        public bool IsSetter { get; set; }

        public MethodDesc(MethodDefinition m)
        {
            Signature = m.ToString();
            IsPublic = m.IsPublic;
            IsPrivate = m.IsPrivate;
            IsGetter = m.IsGetter;
            IsSetter = m.IsSetter;
        }

        public MethodDesc(MethodReference m)
        {
            Signature = m.ToString();
        }

        public override string ToString()
        {
            string access = (IsPublic ? "PUBLIC " : "") + (IsPrivate ? "PRIVATE " : "");
            if (access.Length == 0) access = "INTERNAL ";
            return access + Signature;
        }
    }
}
