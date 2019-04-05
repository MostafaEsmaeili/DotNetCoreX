using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Enum;
using LinqToDB.Mapping;

namespace Framework.AccessManagement.Entity
{
    [Table("Resource",Schema = "sec")]

    public  class ApiResource : BaseResource
    {
        public ApiResource()
        {
            Type = ResourceType.Api;
        }
        [Column,     NotNull    ]
        [MaxLength(128)]
        [StringLength(128)]public string Title      { get; set; } // nvarchar(128)
        [Column,        Nullable]
        [MaxLength(128)]
        [StringLength(128)]
        public string Address    { get; set; } // nvarchar(200)
        [Column,        Nullable] 
        [MaxLength(8)]
        [StringLength(8)]
        public string MethodType { get; set; } // nvarchar(8)
        [LinqToDB.Mapping.Association(ThisKey="Id", OtherKey="ResourceId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
        public virtual ICollection<AccessControl> AccessControls { get; set; } = new HashSet<AccessControl>();
    }
}