using AirbnbDiploma.BLL.Services.UserService;
using AirbnbDiploma.Core.Dto.Stays;
using AirbnbDiploma.Core.Dto.Users;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.FilteringInfo;
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
        var usedId = _userService.GetUserId();
        var stay = new Stay
        {
            Title = stayDto.Title,
            Description = stayDto.Description,
            CoverImageUrl = stayDto.CoverImageUrl,
            Location = stayDto.Location,
            OwnerId = usedId,
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
            Beds = stay.Beds,
            Bedrooms = stay.Bedrooms,
            Bathrooms = stay.Bathrooms,
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
            Owner = new UserInfoDto
            {
                Id = stay.Owner.Id,
                UserName = stay.Owner.UserName,
                Email = stay.Owner.Email,
                ImageUrl = stay.Owner.ImageUrl,
                Gender = stay.Owner.Gender,
                DateOfBirth = stay.Owner.DateOfBirth,
                YearsOfHosting = (int)((DateTime.UtcNow - stay.Owner.RegisteredAt).TotalDays / 365.2425)
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

    public async Task<IEnumerable<StayBriefDto>> GetStaysAsync(StayFilteringInfo filteringInfo)
    {
        var stays = await _unitOfWork.StaysRepository.GetAllFilteredAsync(filteringInfo);
        return stays.Select((stay) => MapStay(stay));
    }

    public async Task<IEnumerable<StayBriefDto>> GetStaysOfAuthorizedUser()
    {
        var userId = _userService.GetUserId();
        var stays = await _unitOfWork.StaysRepository.GetAllByOwnerId(userId);
        return stays.Select((stay) => MapStay(stay));
    }

    public async Task RemoveStayByIdAsync(int id)
    {
        var stay = await _unitOfWork.StaysRepository.GetByIdAsync(id);
        _userService.ValidateUserId(stay.OwnerId);
        await _unitOfWork.StaysRepository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }

    private static StayBriefDto MapStay(Stay stay)
    {
        return new StayBriefDto
        {
            Id = stay.Id,
            ImageUrl = stay.CoverImageUrl,
            Name = stay.Title,
            Place = stay.Location,
            Status = stay.Status,
            StartDate = stay.StartDate,
            EndDate = stay.EndDate,
            Rating = stay.OverallRating,
            Price = stay.Price,
        };
    }
}
