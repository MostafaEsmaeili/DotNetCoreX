using System;
using System.ComponentModel.DataAnnotations;
using Framework.Entity;
using Framework.Enum;
using  LinqToDB;
using LinqToDB.Mapping;

namespace Framework.AccessManagement.Entity
{
    [Table("Resource",Schema = "sec")]
    public abstract class BaseResource : IEntity<int>,IHaveFullAudit
    {
        public  int Id { get; set; }
        [MaxLength(128)]
        [StringLength(128)]
        public string Name { get; set; }
        public ResourceType Type { get; set; }

        [Column,NotNull]
        [MaxLength(128)]
        [StringLength(128)]

        public  string CreatedBy { get; set; }
        [Column,NotNull]
        public DateTime CreatedAt { get; set; }
        [Column,NotNull]
        [MaxLength(128)]
        [StringLength(128)]
        public string  ModifiedBy { get; set; }
        [Column,NotNull]
        public DateTime  ModifiedAt { get; set; }
        [Column,Nullable]
        [MaxLength(64)]
        [StringLength(64)]
        public string Ip { get; set; }
    }
}