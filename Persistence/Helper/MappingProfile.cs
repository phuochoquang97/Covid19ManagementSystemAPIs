using System;
using AutoMapper;
using Covid_Project.Domain.Models;
using Covid_Project.Domain.Models.DTOs;

namespace Covid_Project.Persistence.Helper
{
    public class MappingProfile : AutoMapper.Profile
    {
        const int POSITIVE_STATE = 1;
        const int NEGATIVE_STATE = 0;
        const string POSITIVE = "Positive";
        const string NEGATIVE = "Negative";
        const string PENDING = "Đang đợi xét duyệt";
        const string TESTING = "Đang xét nghiệm";
        const string DONE = "Đã có kết quả";
        const string WAIT_FOR_RESULT = "Chưa có kết quả";
        const int PENDING_STATE = 0;
        const int TESTING_STATE = 1;
        const int DONE_STATE = 2;
        const int MALE_ID = 0;
        const string MALE = "Nam";
        const int FEMALE_ID = 1;
        const string FEMALE = "Nữ";
        const int OTHER_ID = 2;
        const string OTHER = "Khác";
        const int NA = -1;
        public MappingProfile()
        {
            CreateMap<TestingLocationDto, TestingLocation>();
            CreateMap<TestingLocation, TestingLocationDto>();

            CreateMap<MedicalInfo, MedicalInfoDto>();

            CreateMap<Role, RoleDto>();

            CreateMap<CityDto, City>()
            .ForMember(dest => dest.Status, act => act.MapFrom(
                src => (src.Status.Equals(POSITIVE) ? POSITIVE_STATE : NEGATIVE_STATE)
            ));

            CreateMap<City, CityDto>()
            .ForMember(dest => dest.Status, act => act.MapFrom(
                src => (src.Status == NEGATIVE_STATE ? NEGATIVE : POSITIVE)
            ));

            CreateMap<TestingRegisterDto, Testing>();
            CreateMap<Testing, TestingResultDto>()
            .ForMember(dest => dest.TestingState, act => act.MapFrom(
                src => (src.TestingState == PENDING_STATE ? PENDING : src.TestingState == TESTING_STATE ? TESTING : DONE)
            ))
            .ForMember(dest => dest.TestingStateId, act => act.MapFrom(
                src => (src.TestingState == 0 ? 0 : src.TestingState == 1 ? 1 : 2)
            ))
            .ForMember(dest => dest.Result, act => act.MapFrom(
                src => ((src.TestingState == PENDING_STATE || src.TestingState == TESTING_STATE) ?
                WAIT_FOR_RESULT : (src.TestingState == DONE_STATE && src.Result == false ? NEGATIVE : POSITIVE))
            ))
            .ForMember(dest => dest.TestingLocation, act => act.MapFrom(
                src => src.TestingLocation != null ? src.TestingLocation : null
            ));

            CreateMap<TestingResultDto, Testing>()
            .ForMember(dest => dest.TestingState, act => act.MapFrom(
                src => (src.TestingState.Equals(PENDING) ? PENDING_STATE : src.TestingState.Equals(TESTING) ? TESTING_STATE : DONE_STATE)
            ))
            .ForMember(dest => dest.Result, act => act.MapFrom(
                src => (src.Result.Equals(NEGATIVE) ? NEGATIVE_STATE : POSITIVE_STATE)
            ));

            CreateMap<MedicalInfo, MedicalInfoDto>();
            CreateMap<MedicalInfoDto, MedicalInfo>();
            CreateMap<Testing, TestingDto>()
            .ForMember(dest => dest.TestingLocation, act => act.MapFrom(
                src => (src.TestingLocation)
            ));
            CreateMap<TestingDto, Testing>();
            CreateMap<LocationCheckin, LocationCheckinDto>();
            CreateMap<LocationCheckinDto, LocationCheckin>();

            CreateMap<Domain.Models.Profile, ProfileDto>()
            .ForMember(dest => dest.Gender, act => act.MapFrom(
                src => (src.Gender == MALE_ID ? MALE : src.Gender == FEMALE_ID ? FEMALE : src.Gender == OTHER_ID ? OTHER : null)
            ));

            CreateMap<ProfileDto, Domain.Models.Profile>()
            .ForMember(dest => dest.Gender, act => act.MapFrom(
                src => src.Gender.ToLower().Equals(MALE.ToLower()) ? MALE_ID : src.Gender.ToLower().Equals(FEMALE.ToLower()) ? FEMALE_ID : src.Gender.ToLower().Equals(OTHER.ToLower()) ? OTHER_ID : NA
            ));

            CreateMap<Itinerary, UserItineraryDto>();
        }
    }
}