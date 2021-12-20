using System.Collections.Generic;

namespace AgilityWeb.Domain.Base.Validator
{
    public class AbstractValidator<T> where T : EntityDtoBase
    {
        public Dictionary<string, string> Errors { get; set; } = new();
        public bool IsValid { get; set; }

        public virtual void Validation(T obj)
        {
        }
    }
}