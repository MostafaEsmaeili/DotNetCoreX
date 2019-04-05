using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Entity;
using LinqToDB.Mapping;

namespace Framework.AccessManagement.Entity
{
    [Table(Schema="sec", Name="AccessControl")]
    public  class AccessControl : IEntity<int> , IHaveFullAudit
    {
        public int Id { get; set; }
        [Column,     NotNull ] public int    ResourceId { get; set; } // int
        [Column,     NotNull ] public string RoleName   { get; set; } // nvarchar(128)
        [Column,     NotNull ] public string UserName   { get; set; } // nvarchar(128)
        [Column,     NotNull ] public int    Access     { get; set; } // int
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

        [LinqToDB.Mapping.Association(ThisKey="ResourceId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="FK_AccessControl_ApiResource", BackReferenceName="AccessControls")]
        public virtual ApiResource ApiResource { get; set; }
        [LinqToDB.Mapping.Association(ThisKey="ResourceId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="FK_AccessControl_MenuResource", BackReferenceName="AccessControls")]
        public virtual MenuResource MenuResource { get; set; }
        [LinqToDB.Mapping.Association(ThisKey="ResourceId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="FK_AccessControl_PageResource", BackReferenceName="AccessControls")]
        public virtual PageResource PageResource { get; set; }
        [LinqToDB.Mapping.Association(ThisKey="ResourceId", OtherKey="Id", CanBeNull=false, Relationship=Relationship.ManyToOne, KeyName="FK_AccessControl_ElementResource", BackReferenceName="AccessControls")]
        public virtual ElementResource ElementResource { get; set; }

    }
}