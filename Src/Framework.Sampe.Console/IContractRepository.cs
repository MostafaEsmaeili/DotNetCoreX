using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Framework.Dapper.Repositories;
using Framework.Entity;
using LinqToDB.Mapping;

namespace Framework.Sampe.Console
{
	public interface IContractRepository : IBaseRepository<Cotnract, int>
	{

	}

	[LinqToDB.Mapping.Table("tblContracts", Schema = "dbo")]
	public class Cotnract : IEntity<int>
	{
		[LinqToDB.Mapping.Column, NotNull]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		[PrimaryKey]
		public int Id { get; set; }

		[LinqToDB.Mapping.Column, NotNull] public DateTime CreateDate { get; set; }
	}
}