using System;
using System.Collections.Generic;
using Framework.Enum;
using LinqToDB.Mapping;

namespace Framework.AccessManagement.Entity
{	
    [Table(Schema="sec", Name="PageResource")]
    public partial class PageResource : BaseResource
    {
        public PageResource()
        {
            Type = ResourceType.Page;
        }
        [Column,     NotNull    ] public string Title    { get; set; } // nvarchar(128)
        [Column,        Nullable] public string PageLink { get; set; } // nvarchar(128)
        [Association(ThisKey="Id", OtherKey="ResourceId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
        public virtual ICollection<AccessControl> AccessControls { get; set; } = new HashSet<AccessControl>();
        [Association(ThisKey="Id", OtherKey="PageResourceId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
        public virtual ICollection<ElementResource> ElementResources { get; set; } = new HashSet<ElementResource>();
        [Association(ThisKey="Id", OtherKey="MenuPageId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
        public virtual ICollection<MenuResource> MenuResources { get; set; } = new HashSet<MenuResource>();
    }
}