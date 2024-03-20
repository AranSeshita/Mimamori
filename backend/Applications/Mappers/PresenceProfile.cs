using AutoMapper;
using Microsoft.Graph.Models;
using Mimamori.Applications.Contracts;

namespace Mimamori.Applications.Mappers;

public class PresenceProfile : Profile
{
    public PresenceProfile()
    {
        CreateMap<Presence, PresenceDto>()
        .ForMember(id => id.Id, opt => opt.MapFrom(id => Guid.NewGuid()))
        .ForMember(userId => userId.UserId, opt => opt.MapFrom(id => id.Id));
    }
}