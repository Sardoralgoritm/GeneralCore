using AutoMapper;

namespace GeneralCore.Mapping;

/// <summary>
/// DTO → Entity (create/update) mapping uchun base config.
/// IMapper.Map&lt;TEntity&gt;(dto) va IMapper.Map(dto, entity) orqali ishlatiladi.
/// </summary>
public abstract class WriteDtoConfig<TDto, TEntity> : Profile
    where TDto : class
    where TEntity : class
{
    protected WriteDtoConfig()
    {
        var map = CreateMap<TDto, TEntity>();
        AlterMapping(map);
    }

    protected virtual void AlterMapping(IMappingExpression<TDto, TEntity> map) { }
}
