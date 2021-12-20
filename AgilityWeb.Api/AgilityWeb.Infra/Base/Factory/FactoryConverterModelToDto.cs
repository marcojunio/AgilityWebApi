using System.Collections.Generic;
using System.Linq;
using AgilityWeb.Domain.Base;

namespace AgilityWeb.Infra.Base.Factory
{
    public abstract class FactoryConverterModelToDto<TEnt, TDto> where TEnt : new() where TDto : new()
    {
        public TDto Parse(TEnt obj)
        {
            return obj == null ? default : Set(new TDto(), obj);
        }

        public TEnt Parse(TDto obj)
        {
            return obj == null ? default : Set(new TEnt(), obj);
        }
        
        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<TEnt> Parse(List<TDto> list)
        {
            return list == null 
                ? new List<TEnt>() 
                : list.Select(Parse).ToList();
        }

        /// <summary>
        /// Parse
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<TDto> Parse(List<TEnt> list)
        {
            return list == null
                ? new List<TDto>()
                : list.Select(Parse).ToList();
        }

        /// <summary>
        /// Set
        /// </summary>
        /// <returns></returns>
        public abstract TEnt Set(TEnt target, TDto source);

        /// <summary>
        /// Set
        /// </summary>
        /// <returns></returns>
        public abstract TDto Set(TDto target, TEnt source);
    }
}