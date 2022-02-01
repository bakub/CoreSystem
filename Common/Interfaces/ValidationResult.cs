using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integrations.Interfaces
{
    public class ValidationResult
    {
        ValidationResult(bool isValid)
        {
            IsValid = isValid;
        }

        public bool IsValid { get; }


        public static ValidationResult Success() => new(true);
        public static ValidationResult Fail() => new(false);
    }
}
