using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Enum;
using LinqToDB.Mapping;

namespace Framework.AccessManagement.Entity
{
    [Table(Schema="sec", Name="ElementResource")]
    public partial class ElementResource : BaseResource
    {
        public ElementResource()
        {
            Type = ResourceType.Element;
        }
        [Column,     NotNull    ] 
        [MaxLength(128)]
        [StringLength(128)]
        public string Title          { get; set; } // nvarchar(128)

        [Column,        Nullable] public string ElementTitleEn { get; set; } // nvarchar(128)
        [Column,        Nullable] public int?   PageResourceId { get; set; } // int
        [LinqToDB.Mapping.Association(ThisKey="PageResourceId", OtherKey="Id", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="FK_ElementResource_PageResource", BackReferenceName="ElementResources")]
        public virtual PageResource PageResource { get; set; }
        [LinqToDB.Mapping.Association(ThisKey="Id", OtherKey="ResourceId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
        public virtual ICollection<AccessControl> AccessControls { get; set; } = new HashSet<AccessControl>();
    }
}