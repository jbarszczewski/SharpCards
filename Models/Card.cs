using System.ComponentModel.DataAnnotations;
using SharpCards.Database;

namespace SharpCards.Models;

public record Card
{
	[Required]
	public Guid Id { get; set; }
	[Required]
	[MinLength(1)]
	public string FrontText { get; set; } = string.Empty;
	[Required]
	[MinLength(1)]
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

	public CardDto ToDto()
	{
		return new CardDto
		{
			Id = Id,
			FrontText = FrontText,
			BackText = BackText
		};
	}
}