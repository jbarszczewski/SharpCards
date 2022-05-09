using SharpCards.Database;

namespace SharpCards.Models
{
	public record Card
	{
		public Guid Id { get; set; }
		public string FrontText { get; set; } = string.Empty;
		public string BackText { get; set; } = string.Empty;

		public static Card FromDto(CardDto dto)
		{
			return new Card
			{
				Id = dto.Id,
				FrontText = dto.FrontText,
				BackText = dto.BackText
			};
		}
	}
}