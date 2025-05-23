﻿namespace LearningEntityFramework.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Value { get; set; }
    public virtual List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
}