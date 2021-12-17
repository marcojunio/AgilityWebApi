using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgilityWeb.Infra.Base.Validator
{
    public class AbstractValidator<T> where T : EntityBase
    {
        public bool IsValid { get; set; } = true;

        public virtual List<string> Validation(T obj)
        {
            return default;
        }
    }
}