using AirbnbDiploma.Core.Entities;
using AirbnbDiploma.Core.FilteringInfo;

namespace AirbnbDiploma.DAL.Filters;
internal class StayFilter
{
    private IQueryable<Stay> _queryable;

    public StayFilter(IQueryable<Stay> queryable)
    {
        _queryable = queryable;
    }

    public IQueryable<Stay> ApplyFilters(StayFilteringInfo filteringInfo)
    {
        ApplyPlaceTypeFilters(filteringInfo);
        ApplyRegionFilters(filteringInfo);
        ApplyDateFilters(filteringInfo);
        ApplyGuestFilters(filteringInfo);
        ApplyPriceFilters(filteringInfo);
        ApplyBedroomFilters(filteringInfo);
        ApplyBedFilters(filteringInfo);
        ApplyBathroomsFilters(filteringInfo);
        ApplyInstantBookFilters(filteringInfo);
        ApplySelfCheckInFilters(filteringInfo);
        ApplyPropertyTypeFilters(filteringInfo);
        ApplyPaginationFilters(filteringInfo);
        return _queryable;
    }

    private void ApplyPaginationFilters(StayFilteringInfo filteringInfo)
    {
        if (filteringInfo.Page > 0)
        {
            _queryable = _queryable.Skip(filteringInfo.Count * (filteringInfo.Page - 1));
        }
        _queryable = _queryable.Take(filteringInfo.Count);
    }

    private void ApplyPlaceTypeFilters(StayFilteringInfo filteringInfo)
    {
        if (filteringInfo.PlaceType is not null)
        {
            _queryable = _queryable.Where(stay => stay.PlaceType == filteringInfo.PlaceType);
        }
    }

    private void ApplyRegionFilters(StayFilteringInfo filteringInfo)
    {
        if (filteringInfo.RegionId is not null)
        {
            _queryable = _queryable.Where(stay => stay.RegionId == filteringInfo.RegionId);
        }
    }

    private void ApplyDateFilters(StayFilteringInfo filteringInfo)
    {
        if (filteringInfo.CheckInDate is not null)
        {
            _queryable = _queryable.Where(g => g.StartDate >= filteringInfo.CheckoutDate);
        }

        if (filteringInfo.CheckoutDate is not null)
        {
            _queryable = _queryable.Where(g => g.EndDate <= filteringInfo.CheckoutDate);
        }
    }

    private void ApplyGuestFilters(StayFilteringInfo filteringInfo)
    {
        _queryable = _queryable.Where(g => g.MaxGuests >= filteringInfo.MinGuests);
    }

    private void ApplyPriceFilters(StayFilteringInfo filteringInfo)
    {
        _queryable = _queryable.Where(g => g.Price >= filteringInfo.MinPrice && g.Price <= filteringInfo.MaxPrice);
    }

    private void ApplyBedroomFilters(StayFilteringInfo filteringInfo)
    {
        _queryable = _queryable.Where(g => g.Bedrooms >= filteringInfo.MinBedrooms);
    }

    private void ApplyBedFilters(StayFilteringInfo filteringInfo)
    {
        _queryable = _queryable.Where(g => g.Beds >= filteringInfo.MinBeds);
    }

    private void ApplyBathroomsFilters(StayFilteringInfo filteringInfo)
    {
        _queryable = _queryable.Where(g => g.Bathrooms >= filteringInfo.MinBathrooms);
    }

    private void ApplyInstantBookFilters(StayFilteringInfo filteringInfo)
    {
        if (filteringInfo.InstantBook is not null)
        {
            _queryable = _queryable.Where(g => g.InstantBook == filteringInfo.InstantBook);
        }
    }

    private void ApplySelfCheckInFilters(StayFilteringInfo filteringInfo)
    {
        if (filteringInfo.SelfCheckIn is not null)
        {
            _queryable = _queryable.Where(g => g.SelfCheckIn == filteringInfo.SelfCheckIn);
        }
    }

    private void ApplyPropertyTypeFilters(StayFilteringInfo filteringInfo)
    {
        if (filteringInfo.PropertyTypes is not null)
        {
            _queryable = _queryable.Where(g => filteringInfo.PropertyTypes.Contains(g.PropertyType));
        }
    }
}
