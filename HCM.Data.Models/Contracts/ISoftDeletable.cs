namespace HCM.Data.Models.Contracts
{
	public interface ISoftDeletable
	{
		bool IsDeleted { get; set; }

		DateTime? DeletedOnUtc { get; set; }
	}
}
