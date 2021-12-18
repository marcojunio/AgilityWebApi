using AgilityWeb.Domain.Base;
using AgilityWeb.Infra.Base;

namespace AgilityWeb.Infra.Services
{
    public class RepositoryService<TEnt, TDto> where TEnt : EntityBase, new()
        where TDto : EntityDtoBase, new()
    {
        /// <summary>
        /// Construtor
        /// </summary>
        public RepositoryService()
        {
            
        }
    }
}