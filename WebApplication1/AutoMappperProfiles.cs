// AutoMapperProfiler.cs
using AutoMapper;
using WebApplication1.Models;
using WebApplication1.DTOs;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<FilmDTO, Film>().ReverseMap();
        CreateMap<ItemDTO, Item>().ReverseMap();

    }
}

