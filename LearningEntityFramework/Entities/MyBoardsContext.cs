﻿using Microsoft.EntityFrameworkCore;

namespace LearningEntityFramework.Entities;

public class MyBoardsContext(DbContextOptions<MyBoardsContext> options): DbContext(options)
{
    public DbSet<WorkItem> WorkItems { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Address> Addresses { get; set; }
}