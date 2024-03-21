using AutoMapper;
using Mimamori.Applications.Contracts;
using Mimamori.Domains.Entities;

namespace Mimamori.Applications.Mappers;

public class PresenceProfile : Profile
{
    public PresenceProfile()
    {
        CreateMap<Microsoft.Graph.Models.Presence, PresenceDto>()
        .ForMember(id => id.Id, opt => opt.MapFrom(id => Guid.NewGuid()))
        .ForMember(userId => userId.UserId, opt => opt.MapFrom(id => id.Id));
        CreateMap<PresenceDto, Presence>();
    }
}