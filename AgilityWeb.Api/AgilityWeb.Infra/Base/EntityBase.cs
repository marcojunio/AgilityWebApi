using System;
using System.Collections.Generic;

namespace AgilityWeb.Infra.Base
{
    public abstract class EntityBase
    {
        public string Id { get; set; }
        public DateTime? DateInsert { get; set; }
        public DateTime? DateEdition { get; set; }
        public List<string> ErrorReason { get; set; }
    }
}