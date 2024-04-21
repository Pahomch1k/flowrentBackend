using AirbnbDiploma.BLL.Services.UserService;
using AirbnbDiploma.Core.Dto.Stays;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.DAL.UnitOfWork;

namespace AirbnbDiploma.BLL.Services.StaysService;

public class StayService : IStayService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public StayService(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public async Task AddStayAsync(NewStayDto stayDto)
    {
        var stay = new Stay
        {
            Title = stayDto.Title,
            Description = stayDto.Description,
            CoverImageUrl = stayDto.CoverImageUrl,
            Location = stayDto.Location,
            StartDate = stayDto.StartDate,
            EndDate = stayDto.EndDate,
            MaxGuests = stayDto.MaxGuests,
            Price = stayDto.Price,
            CleaningFee = stayDto.CleaningFee,
            OverallRating = 0,
            CleanlinessRating = 0,
            AccuracyRating = 0,
            CheckInRating = 0,
            CommunicationRating = 0,
            LocationRating = 0,
            ValueRating = 0,
        };
        await _unitOfWork.StaysRepository.AddAsync(stay);
        await _unitOfWork.CommitAsync();
    }

    public async Task<StayDto> GetStayAsync(int id)
    {
        var stay = await _unitOfWork.StaysRepository.GetByIdAsync(id);
        return new StayDto
        {
            Id = stay.Id,
            Title = stay.Title,
            Description = stay.Description,
            Location = stay.Location,
            MaxGuests = stay.MaxGuests,
            Price = stay.Price,
            CleaningFee = stay.CleaningFee,
            Rating = new RatingDto
            {
                Accuracy = stay.AccuracyRating,
                CheckIn = stay.CheckInRating,
                Cleanliness = stay.CleanlinessRating,
                Communication = stay.CommunicationRating,
                Location = stay.LocationRating,
                Value = stay.ValueRating,
                Overall = stay.OverallRating,
            },
            ImageUrls = stay.ImageUrls.Select((image) => image.Url),
            Amenities = stay.Amenities.GroupBy((amenity) => amenity.TagTypeId).Select((amenityGroup) => new TagTypeDto
            {
                Name = amenityGroup.First().Name,
                Tags = amenityGroup.Select((amenityGroupElement) => new TagDto
                {
                    Name = amenityGroupElement.Name,
                    Category = amenityGroupElement.Category,
                })
            }),
        };
    }

    public async Task<IEnumerable<StayBriefDto>> GetStaysAsync(int skip, int count)
    {
        var stays = await _unitOfWork.StaysRepository.GetAllAsync(skip, count);
        return stays.Select((stay) => new StayBriefDto
        {
            Id = stay.Id,
            ImageUrl = stay.CoverImageUrl,
            Name = stay.Title,
            Place = stay.Location,
            StartDate = stay.StartDate,
            EndDate = stay.EndDate,
            Rating = stay.OverallRating,
            Price = stay.Price,
        });
    }

    public async Task RemoveStayByIdAsync(int id)
    {
        var stay = await _unitOfWork.StaysRepository.GetByIdAsync(id);
        _userService.ValidateUserId(stay.OwnerId);
        await _unitOfWork.StaysRepository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }
}
