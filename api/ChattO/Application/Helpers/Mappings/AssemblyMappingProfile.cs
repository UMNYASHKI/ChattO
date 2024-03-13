using AutoMapper;
using System.Reflection;

namespace Application.Helpers.Mappings;

public class AssemblyMappingProfile : Profile
{
    public AssemblyMappingProfile(Assembly assembly)
        => ApplyMappingsFromAssembly(assembly);

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetExportedTypes()
            .Where(t => t.GetInterfaces()
                           .Any(i => i.IsGenericType 
                           && i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
            .ToList();
    }
}
