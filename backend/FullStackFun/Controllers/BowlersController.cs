using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace FullStackFun.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BowlersController : ControllerBase
    {
        private readonly string _dbPath;

        public BowlersController(IConfiguration configuration)
        {
            _dbPath = configuration["DatabasePath"] ?? "BowlingLeague.sqlite";
        }

        [HttpGet]
        public IActionResult Get()
        {
            var bowlers = new List<object>();

            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT 
                    b.BowlerFirstName,
                    b.BowlerMiddleInit,
                    b.BowlerLastName,
                    t.TeamName,
                    b.BowlerAddress,
                    b.BowlerCity,
                    b.BowlerState,
                    b.BowlerZip,
                    b.BowlerPhoneNumber
                FROM Bowlers b
                JOIN Teams t ON b.TeamID = t.TeamID
                WHERE t.TeamName IN ('Marlins', 'Sharks')
                ORDER BY t.TeamName, b.BowlerLastName";

            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                bowlers.Add(new
                {
                    firstName = reader.GetString(0),
                    middleInit = reader.IsDBNull(1) ? "" : reader.GetString(1),
                    lastName = reader.GetString(2),
                    teamName = reader.GetString(3),
                    address = reader.IsDBNull(4) ? "" : reader.GetString(4),
                    city = reader.IsDBNull(5) ? "" : reader.GetString(5),
                    state = reader.IsDBNull(6) ? "" : reader.GetString(6),
                    zip = reader.IsDBNull(7) ? "" : reader.GetString(7),
                    phone = reader.IsDBNull(8) ? "" : reader.GetString(8)
                });
            }

            return Ok(bowlers);
        }
    }
}