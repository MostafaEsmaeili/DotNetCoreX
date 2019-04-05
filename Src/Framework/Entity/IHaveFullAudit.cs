using System;
using System.ComponentModel.DataAnnotations;
using LinqToDB.Mapping;

namespace Framework.Entity
{
    public interface IHaveFullAudit
    {
        [Column,NotNull]
        [MaxLength(128)]
        [StringLength(128)]

         string CreatedBy { get; set; }
        [Column,NotNull]
         DateTime CreatedAt { get; set; }
        [Column,NotNull]
        [MaxLength(128)]
        [StringLength(128)]
        string  ModifiedBy { get; set; }
        [Column,NotNull]
        DateTime  ModifiedAt { get; set; }
        [Column,Nullable]
        [MaxLength(64)]
        [StringLength(64)]
        string Ip { get; set; }


    }
}