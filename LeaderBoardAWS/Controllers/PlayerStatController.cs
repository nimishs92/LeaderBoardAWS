using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaderBoardAWS.Entity;
using LeaderBoardAWS.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeaderBoardAWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerStatController : ControllerBase
    {
        private IPlayerStatsDynamoService _playerStatsService;
        public PlayerStatController(PlayerStatsDynamoService playerStatsService)
        {
            this._playerStatsService = playerStatsService;
        }
        [HttpGet("{id}")]
        //[Route("api/[controller]/id")]
        public IEnumerable<PlayerStat> GetPlayerStats(string id)
        {
            return _playerStatsService.GetPlayerStats(id);
        }

        [HttpGet("{id}/{duration}")]
        public IEnumerable<PlayerStat> GetPlayerStatsDuration(string id, int duration)
        {
            return _playerStatsService.GetPlayerStats(id, duration);
        }

        [HttpPost]
        public ActionResult AddPlayerStat([FromBody]PlayerStat playerStat)
        {
            try
            {
                _playerStatsService.Create(playerStat);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}