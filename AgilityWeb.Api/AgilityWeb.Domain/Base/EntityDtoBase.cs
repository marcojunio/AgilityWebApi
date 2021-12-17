using System;

namespace AgilityWeb.Domain.Base
{
    public abstract class EntityDtoBase
    {
        public string Id { get; set; }
        public DateTime? DateInsert { get; set; }
        public DateTime? DateEdition { get; set; }
    }
}