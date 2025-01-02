
namespace ColorP.Services
{
    public interface IColorService
    {
        Task<IEnumerable<Color>> GetColorsAsync();
        Task AddColorAsync(Color color);
        Task UpdateColorAsync(Color color);
        Task UpdateDisplayOrderAsync(int id, int displayOrder);
        Task DeleteColorAsync(int id);
    }
}
