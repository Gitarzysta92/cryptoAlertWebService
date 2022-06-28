using AutoMapper;
using Database.Models;

namespace Coins.Models.Mappings;

public class ColorThemeProfile : Profile
{
	public ColorThemeProfile()
	{
		CreateMap<ColorTheme, ColorThemeDto>();
	}
}