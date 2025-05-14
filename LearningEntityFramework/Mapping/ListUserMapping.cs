using LearningEntityFramework.Dto;
using LearningEntityFramework.Entities;

namespace LearningEntityFramework.Mapping;

public static class ListUserMapping
{
    public static PagedResult<T> ToPagedResultDto<T>(this List<T> items,
        int totalCount, int pageSize, int pageNumber)
    {
        return new PagedResult<T>(
            items,
            (int)Math.Ceiling(totalCount/(double)pageSize),
            pageSize * (pageNumber - 1) + 1,
            pageSize * pageNumber,
            totalCount
        );
    }
}