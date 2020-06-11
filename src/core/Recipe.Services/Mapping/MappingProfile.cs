using System;
using AutoMapper;
using Recipe.Services.Models;
using Recipe.Data;
using Recipe.Data.Recipes;

namespace Recipe.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RecipeVariation, RecipeModel>()
                .ForMember(r => r.Name, opt => opt.MapFrom(rv => rv.Recipe.Name))
                .ForMember(r => r.Year, opt => opt.MapFrom(rv => rv.Year))
                .ForMember(r => r.Country, opt => opt.MapFrom(rv => rv.Country))
                .ForMember(r => r.TimeOfCooking, opt => opt.MapFrom(rv => rv.TimeOfCooking))
                .ForMember(r => r.Author, opt => opt.MapFrom(rv => rv.Author.Name))
                .ForMember(r => r.CookingDescription, opt => opt.MapFrom(rv => rv.CookingDescription))
                .ForMember(r => r.Ingredients, opt => opt.MapFrom(rv => rv.Ingredients));

            CreateMap<RecipeModel, RecipeVariation>()
                .ForMember(r => r.Author, opt => opt.Ignore())
                .ForMember(r => r.CreatedAt, opt => opt.MapFrom(r => DateTime.UtcNow))
                .ForMember(r => r.CreatedBy, opt => opt.MapFrom(r => 1)) // TODO: Map from current user
                .ForAllOtherMembers(opt => opt.MapAtRuntime());

            CreateMap<Ingredient, IngredientModel>()
                .ReverseMap();

            CreateMap<RecipeModel, Data.Recipes.Recipe>()
                .ForMember(r => r.Name, opt => opt.MapFrom(r => r.Name))
                .ForMember(r => r.Id, opt => opt.Ignore())
                .ForMember(r => r.Variations, opt => opt.Ignore());
        }
    }
}