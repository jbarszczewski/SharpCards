namespace SharpCards.Models
{
	public class CardContent
	{
		public Guid Id { get; set; }
		public string FrontText { get; set; } = string.Empty;
		public string BackText { get; set; } = string.Empty;
	}
}