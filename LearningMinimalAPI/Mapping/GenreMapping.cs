﻿using LearningMinimalAPI.Dtos;
using LearningMinimalAPI.Entities;

namespace LearningMinimalAPI.Mapping;

public static class GenreMapping
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(genre.Id, genre.Name);
    }
}