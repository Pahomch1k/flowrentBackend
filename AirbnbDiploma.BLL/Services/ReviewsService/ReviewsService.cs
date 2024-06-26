﻿using AirbnbDiploma.BLL.Services.UserService;
using AirbnbDiploma.Core.Dto.Reviews;
using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.DAL.UnitOfWork;

namespace AirbnbDiploma.BLL.Services.ReviewsService;

public class ReviewsService : IReviewsService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;

    public ReviewsService(IUnitOfWork unitOfWork, IUserService userService)
    {
        _unitOfWork = unitOfWork;
        _userService = userService;
    }

    public async Task AddReviewByStayId(NewReviewDto reviewDto)
    {
        var review = new Review
        {
            CreationDate = reviewDto.CreationDate,
            Description = reviewDto.Description,
            Duration = reviewDto.Duration,
            Rating = reviewDto.Rating,
            StayId = reviewDto.StayId,
        };
        await _unitOfWork.ReviewRepository.AddAsync(review);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<ReviewDto>> GetReviewsByStayIdAsync(Guid stayId, int skip, int count)
    {
        var reviews = await _unitOfWork.ReviewRepository.GetAllByStayIdAsync(stayId, skip, count);
        return reviews.Select((review) => new ReviewDto
        {
            Description = review.Description,
            CreationDate = review.CreationDate,
            Duration = review.Duration,
            Rating = review.Rating,
        });
    }

    public async Task RemoveReviewById(int id)
    {
        var review = await _unitOfWork.ReviewRepository.GetByIdAsync(id);
        _userService.ValidateUserId(review.AuthorId);
        await _unitOfWork.ReviewRepository.DeleteAsync(id);
        await _unitOfWork.CommitAsync();
    }
}
