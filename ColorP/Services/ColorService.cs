using ColorP.Helpers.DB;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ColorP.Services
{
    public class ColorService : IColorService
    {
        private readonly DBHelper _dbHelper;
        public ColorService(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

            _dbHelper = new DBHelper(connectionString);
        }

        public async Task<IEnumerable<Color>> GetColorsAsync()
        {
            string query = "SELECT * FROM Colors ORDER BY DisplayOrder ASC";
            var table = await _dbHelper.ExecuteQueryAsync(query);
            var colors = new List<Color>();
            foreach (DataRow row in table.Rows)
            {
                colors.Add(new Color
                {
                    Id = (int)row["ColorID"],
                    Name = row["ColorName"].ToString()!,
                    Price = (decimal)row["Price"],
                    DisplayOrder = (int)row["DisplayOrder"],
                    IsInStock = (bool)row["IsInStock"],
                    HexCode = row["HexCode"].ToString()!
                });
            }
            return colors;
        }

        public async Task AddColorAsync(Color color)
        {
           
            string query = @"
        INSERT INTO Colors (ColorName, Price, DisplayOrder, IsInStock, HexCode)
        VALUES (@ColorName, @Price, @DisplayOrder, @IsInStock, @HexCode)";

            var parameters = new[]
            {
        new SqlParameter("@ColorName", color.Name),
        new SqlParameter("@Price", color.Price),
        new SqlParameter("@DisplayOrder", color.DisplayOrder),
        new SqlParameter("@IsInStock", color.IsInStock),
        new SqlParameter("@HexCode", color.HexCode)
    };

            await _dbHelper.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task DeleteColorAsync(int id)
        {
            string query = "DELETE FROM Colors WHERE ColorID = @ColorID";
            var parameters = new[]
            {
                new SqlParameter("@ColorID", id)
            };

            await _dbHelper.ExecuteNonQueryAsync(query, parameters);
        }

        public async Task UpdateColorAsync(Color color)
        {
            string query = @"
                UPDATE Colors 
                SET ColorName = @ColorName, 
                    Price = @Price, 
                    DisplayOrder = @DisplayOrder, 
                    IsInStock = @IsInStock,
                    HexCode = @HexCode -- Include HexCode in update
                WHERE ColorID = @ColorID";

            var parameters = new[]
            {
                new SqlParameter("@ColorID", color.Id),
                new SqlParameter("@ColorName", color.Name),
                new SqlParameter("@Price", color.Price),
                new SqlParameter("@DisplayOrder", color.DisplayOrder),
                new SqlParameter("@IsInStock", color.IsInStock),
                new SqlParameter("@HexCode", color.HexCode)
            };

            await _dbHelper.ExecuteNonQueryAsync(query, parameters);
        }
        public async Task UpdateDisplayOrderAsync(int id, int displayOrder)
        {
            string query = "UPDATE Colors SET DisplayOrder = @DisplayOrder WHERE ColorID = @Id";

            var parameters = new[]
            {
        new SqlParameter("@Id", id),
        new SqlParameter("@DisplayOrder", displayOrder)
    };

            await _dbHelper.ExecuteNonQueryAsync(query, parameters);
        }
    }
}
