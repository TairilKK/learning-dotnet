using LearningEntityFramework.Entities;

namespace LearningEntityFramework.Dto;

public record PagedResult<T>(
    List<T> Items,
    int TotalPages,
    int ItemsFrom,
    int ItemsTo,
    int TotalItemsCount
);