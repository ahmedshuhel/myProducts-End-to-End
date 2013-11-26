﻿using AutoMapper;
using MyProducts.Model;
using MyProducts.Services.DTOs;
using System.Linq;

namespace MyProducts.Services
{
    public static class DataMapper
    {
        public static void Configure()
        {
            Mapper.CreateMap<Article, ArticleDetailDto>()
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => Constants.ImagesFolder + "/" + src.ImageUrl))
                .IgnoreAllNonExisting();
            Mapper.CreateMap<Article, ArticleDto>().IgnoreAllNonExisting();
            Mapper.CreateMap<ArticleDetailUpdateDto, Article>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .IgnoreAllNonExisting();
            Mapper.CreateMap<Category, CategoryDto>().IgnoreAllNonExisting();
            Mapper.CreateMap<CategoryDto, Category>().IgnoreAllNonExisting();

            Mapper.AssertConfigurationIsValid();
        }

        public static ArticleDetailDto MapDetails(this Article entity)
        {
            return Mapper.Map<ArticleDetailDto>(entity);
        }

        public static ArticleDto Map(this Article entity)
        {
            return Mapper.Map<ArticleDto>(entity);
        }

        public static Article Map(this ArticleDetailUpdateDto dto)
        {
            return Mapper.Map<Article>(dto);
        }

        public static CategoryDto Map(this Category category)
        {
            return Mapper.Map<CategoryDto>(category);
        }
    }

    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination>
          IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(
                x => x.SourceType.Equals(sourceType) && x.DestinationType.Equals(destinationType));

            foreach (var property in existingMaps.GetUnmappedPropertyNames())
            {
                expression.ForMember(property, opt => opt.Ignore());
            }

            return expression;
        }
    }
}