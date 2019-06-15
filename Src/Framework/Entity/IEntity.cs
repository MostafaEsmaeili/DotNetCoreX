using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinqToDB.Mapping;

namespace Framework.Entity
{
	/// <summary>
	/// Defines interface for base entity type. All entities in the system must implement this interface.
	/// </summary>
	/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
	public interface IEntity<TPrimaryKey> where TPrimaryKey : IComparable
	{
		[LinqToDB.Mapping.Column , NotNull]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		[PrimaryKey]
		/// <summary>Unique identifier for this entity.</summary>
		TPrimaryKey Id { get; set; }
	}
	public interface IEntity
	{
	}
}