
namespace AgilityWeb.Infra
{
    public abstract class FactoryConverterModelToDto<TEnt,TDto> where TEnt : new() where TDto : new()
    {

        public TDto Parse(TEnt obj)
        {
            return obj == null ? 
                default : 
                Set(new TDto(),obj);
        }

        public TEnt Parse(TDto obj)
        {
            return obj == null ? 
                default : 
                Set(new TEnt(), obj);
        }

        public abstract TDto Set(TDto target, TEnt source);
        public abstract TEnt Set(TEnt target, TDto source);
    }
}
