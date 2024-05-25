using AutoMapper;
namespace Application.Helpers.Mappings;

public interface IMapWith<T>
{
    void Mapping(Profile profile)
        => profile.CreateMap(typeof(T), GetType());
}
