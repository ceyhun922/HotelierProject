
using AutoMapper;
using DTOs.AboutDTOs;
using DTOs.AboutImageDTOs;
using DTOs.ChargeDTOs;
using DTOs.ContactDTOs;
using DTOs.FooterDTOs;
using DTOs.MessageDTOs;
using DTOs.RoomDTOs;
using DTOs.RoomTypeDTOs;
using DTOs.SliderDTOs;
using DTOs.StaffDTOs;
using DTOs.TeamDTOs;
using DTOs.TestimonialDTOs;
using EntityLayer.Concrete;

namespace Service.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            //About
            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, GetByIdAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
            //AboutImage
            CreateMap<AboutImage, ResultAboutImageDto>().ReverseMap();
            CreateMap<AboutImage, CreateAboutImageDto>().ReverseMap();
            CreateMap<AboutImage, GetByIdAboutImageDto>().ReverseMap();
            CreateMap<AboutImage, UpdateAboutImageDto>().ReverseMap();
            //Charge
            CreateMap<Charge, ResultChargeDto>().ReverseMap();
            CreateMap<Charge, CreateChargeDto>().ReverseMap();
            CreateMap<Charge, GetByIdChargeDto>().ReverseMap();
            CreateMap<Charge, UpdateChargeDto>().ReverseMap();
            //Contact
            CreateMap<Contact, ResultContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, GetByIdContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
            //Footer
            CreateMap<Contact, ResultFooterDto>().ReverseMap();
            CreateMap<Contact, CreateFooterDto>().ReverseMap();
            CreateMap<Contact, GetByIdFooterDto>().ReverseMap();
            CreateMap<Contact, UpdateFooterDto>().ReverseMap();
            //Room
            CreateMap<Room, ResultRoomDto>().ReverseMap();
            CreateMap<Room, CreateRoomDto>().ReverseMap();
            CreateMap<Room, GetByIdRoomDto>().ReverseMap();
            CreateMap<Room, UpdateRoomDto>().ReverseMap();
            //RoomType
            CreateMap<RoomType, ResultRoomTypeDto>().ReverseMap();
            CreateMap<RoomType, CreateRoomTypeDto>().ReverseMap();
            CreateMap<RoomType, GetByIdRoomTypeDto>().ReverseMap();
            CreateMap<RoomType, UpdateRoomTypeDto>().ReverseMap();
            //RoomType
            CreateMap<Message, ResultMessageDto>().ReverseMap();
            CreateMap<Message, CreateMessageDto>().ReverseMap();
            CreateMap<Message, GetByIdMessageDto>().ReverseMap();
            CreateMap<Message, UpdateMessageDto>().ReverseMap();
            //Team
            CreateMap<Team, ResultTeamDto>().ReverseMap();
            CreateMap<Team, CreateTeamDto>().ReverseMap();
            CreateMap<Team, GetByIdTeamDto>().ReverseMap();
            CreateMap<Team, UpdateTeamDto>().ReverseMap();
            //Slider
            CreateMap<Slider, ResultSliderDto>().ReverseMap();
            CreateMap<Slider, CreateSliderDto>().ReverseMap();
            CreateMap<Slider, UpdateSliderDto>().ReverseMap();
            CreateMap<Slider, GetByIdSliderDto>().ReverseMap();
            //Staff
            CreateMap<Staff, ResultStaffDto>().ReverseMap();
            CreateMap<Staff, CreateStaffDto>().ReverseMap();
            CreateMap<Staff, UpdateStaffDto>().ReverseMap();
            CreateMap<Staff, GetByIdStaffDto>().ReverseMap();
            //Testimonail
            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetByIdTestimonialDto>().ReverseMap();

        }
    }
}