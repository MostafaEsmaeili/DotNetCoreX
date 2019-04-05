using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Enum;
using LinqToDB.Mapping;

namespace Framework.AccessManagement.Entity
{	
    [Table(Schema="sec", Name="MenuResource")]
    public  class MenuResource : BaseResource
    {
        public MenuResource()
        {
            Type = ResourceType.Menu;
        }

        [Column,     NotNull    ] 
        [MaxLength(128)]
        [StringLength(128)]
        public string Title               { get; set; } // nvarchar(128)
        [Column,        Nullable] public int?   MenuOrder           { get; set; } // int
        [Column,        Nullable] public string MenuTitleEn         { get; set; } // nvarchar(128)
        [Column,        Nullable] public int?   ParenMenuResourceId { get; set; } // int
        [Column,        Nullable] public int?   MenuPageId          { get; set; } // int
        [Column,        Nullable] public string MenuIcon    { get; set; } // nvarchar(50)

        [LinqToDB.Mapping.Association(ThisKey="MenuPageId", OtherKey="Id", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="FK_MenuResource_PageResource", BackReferenceName="MenuResources")]
        public virtual PageResource MenuPage { get; set; }
        [LinqToDB.Mapping.Association(ThisKey="ParenMenuResourceId", OtherKey="Id", CanBeNull=true, Relationship=Relationship.ManyToOne, KeyName="FK_ParentMenu", BackReferenceName="ParentMenus")]
        public virtual MenuResource ParenMenuResource { get; set; }
        [LinqToDB.Mapping.Association(ThisKey="Id", OtherKey="ResourceId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
        public virtual ICollection<AccessControl> AccessControls { get; set; } = new HashSet<AccessControl>();
        [LinqToDB.Mapping.Association(ThisKey="Id", OtherKey="ParenMenuResourceId", CanBeNull=true, Relationship=Relationship.OneToMany, IsBackReference=true)]
        public virtual ICollection<MenuResource> InverseParenMenuResource { get; set; } = new HashSet<MenuResource>();
    }
}