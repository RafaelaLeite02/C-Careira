using AutoMapper;
using FilmesAPI.DTOs;
using FilmesAPI.Models;

namespace FilmesAPI.Profiles
    //AutoMap para colocar o Dto no controller
{
    public class FilmeProfile : Profile 
    {
        public FilmeProfile() 
        {
            CreateMap<CreateFilmeDTO, Filme>();
            CreateMap<UpdateFilmeDTO, Filme>();
            CreateMap<Filme, UpdateFilmeDTO>();
            CreateMap<Filme, ReadFilmeDTO>();

        }
    }
}
          