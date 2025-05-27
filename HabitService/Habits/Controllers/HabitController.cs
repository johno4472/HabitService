using HabitNetworkAPI.Habits.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using HabitNetworkAPI.Habits.Models;

namespace HabitNetworkAPI.Habits.Controllers
{

    [ApiController]
    [Route("api/v1/[controller]")]
    public class HabitController : ControllerBase
    {
        private readonly IHabitService _habitService;

        public HabitController(IHabitService habitService)
        {
            _habitService = habitService;
        }

        [HttpGet("habit/{userId}/{habitId}")]
        public async Task<IResult> GetHabit(int userId, int habitId)
        {
            var habitDto = await _habitService.GetHabitInfoAsync(userId, habitId);

            return Results.Ok(habitDto);
        }

        [HttpPost("habit/{userId}")]
        public async Task<IResult> CreateHabit(int userId, [FromBody] HabitCreationInfo habitCreationInfo)
        {
            var habitDto = await _habitService.CreateNewHabitAsync(userId, habitCreationInfo);

            //put in results.created to send created response
            return Results.Ok(habitDto);
        }

        [HttpDelete("habit/{userId}/{habitId}")]
        public async Task<IResult> DeleteHabit(int userId, int habitId)
        {
            var habitDeleted = await _habitService.DeleteExistingHabitAsync(userId, habitId);

            //FIICD Not sure this HTTP response is appropriate since I am deleting the habit, so I don't think I should return it as an object
            if (habitDeleted)
            {
                return Results.Ok(habitDeleted);
            }
            return Results.BadRequest();
        }

        [HttpPut("habit/{userId}/{habitId}")]
        public async Task<IResult> UpdateHabit(int userId, int habitId, HabitDto incomingHabitDto)
        {
            var outgoingHabitDto = await _habitService.UpdateExistingHabit(userId, habitId, incomingHabitDto);

            return Results.Ok(outgoingHabitDto);
        }
    }
}
