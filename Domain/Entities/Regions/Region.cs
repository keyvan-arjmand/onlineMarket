using System.ComponentModel.DataAnnotations.Schema;
using Domain.Common;

namespace Domain.Entities.Regions;

public class Region : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int? ParentId { get; set; }
    [ForeignKey("ParentId")] public Region? Parent { get; set; } 
}