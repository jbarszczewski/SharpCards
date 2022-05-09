using DnetIndexedDb;

namespace SharpCards.Database
{
	public record CardDto
	{
		[IndexDbKey(AutoIncrement = false)]
		public Guid Id { get; set; }
		[IndexDbIndex]
		public string FrontText { get; set; } = string.Empty;
		[IndexDbIndex]
		public string BackText { get; set; } = string.Empty;
	}
}