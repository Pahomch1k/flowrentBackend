﻿using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.DAL.Repositories.Base;

namespace AirbnbDiploma.DAL.Repositories.ReviewRepository;

public interface IReviewRepository : IRepository<Review, int>
{
    Task<IEnumerable<Review>> GetAllByStayIdAsync(Guid stayId, int skip, int count);
}
