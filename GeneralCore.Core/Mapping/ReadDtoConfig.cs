using AutoMapper;

namespace GeneralCore.Mapping;

/// <summary>
/// Entity → DTO (read) mapping uchun base config.
/// ProjectTo&lt;TDto&gt;() orqali SQL ga translate bo'ladi.
/// </summary>
public abstract class ReadDtoConfig<TDto, TEntity> : Profile
    where TDto : class
    where TEntity : class
{
    protected ReadDtoConfig()
    {
        var map = CreateMap<TEntity, TDto>();
        AlterMapping(map);
    }

    protected virtual void AlterMapping(IMappingExpression<TEntity, TDto> map) { }
}
