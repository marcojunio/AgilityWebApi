using System.Collections.Generic;
using System.Threading.Tasks;

namespace AgilityWeb.Infra.Base.Validator
{
    public abstract class AbstractValidator<T> where T : EntityBase
    {
        public List<string> Errors { get; set; }
        public bool IsValid { get; set; } = true;

        public abstract void Validation(T obj);
    }
}