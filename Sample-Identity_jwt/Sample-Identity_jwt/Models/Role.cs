using Microsoft.AspNetCore.Identity;
using Sample_Identity_jwt.SmartEnum;

namespace Sample_Identity_jwt.Models
{
    public class Role: Enumeration<Role>
    {
        public static readonly Role Admin = new(1, nameof(Admin));
        public static readonly Role User = new(2, nameof(User));
        public Role(int value, string name):
            base(value, name)
        {
        }
    }
}
