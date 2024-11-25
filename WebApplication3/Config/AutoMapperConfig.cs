using AutoMapper;
using homelisti_API.Models.Domain;
using Ninject;
using System;
using HomelistiAPI.Db;
using HomelistiAPI.Models.Domain;

namespace WebApplication3.Config
{
    public class AutoMapperConfig
    {
        static readonly IKernel kernel = null;
        public static MapperConfiguration MapperConfiguration()
        {
            return new MapperConfiguration(config =>
            {
                config.ConstructServicesUsing(t => kernel.Get(t));
                config.AddProfile(new MappingProfile());
            });
        }
    }
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DateTime?, TimeSpan?>().ConvertUsing(new DateTimeToTimeSpanConverter());
            CreateMap<TimeSpan?, DateTime?>().ConvertUsing(new TimeSpanToDateTimeConverter());

            // Add as many of these lines as you need to map your objects
            CreateMap<Listing, ListingDTO>().ReverseMap();
            CreateMap<ListingType, ListingTypeDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Contact, ContactDTO>().ReverseMap();
            CreateMap<Location, LocationDTO>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<CustomField, CustomFieldDTO>().ReverseMap();
            CreateMap<Choice, ChoiceDTO>().ReverseMap();
            CreateMap<CustomFields_Choices, CustomField_ChoiceDTO>().ReverseMap();
            CreateMap<Valuess, ValueeDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();
        }

        public class DateTimeToTimeSpanConverter : ITypeConverter<DateTime?, TimeSpan?>
        {
            public TimeSpan? Convert(DateTime? source, TimeSpan? destination, ResolutionContext context)
            {
                if (source != null)
                    return source.Value.TimeOfDay;
                else
                    return null;
            }
        }

        public class TimeSpanToDateTimeConverter : ITypeConverter<TimeSpan?, DateTime?>
        {
            public DateTime? Convert(TimeSpan? source, DateTime? destination, ResolutionContext context)
            {
                if (source != null)
                    return new DateTime() + source.Value;
                else
                    return null;
            }
        }
    }
}