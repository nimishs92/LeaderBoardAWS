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
    public class LeaderBoardController : ControllerBase
    {
        private LeaderBoardService _leaderBoardService;
        public LeaderBoardController(LeaderBoardService leaderBoardService)
        {
            this._leaderBoardService = leaderBoardService;
        }
        [HttpGet("{match}/{duration}")]
        public IEnumerable<LeaderBoardEntry> LeaderBoard(string match, int duration)
        {
            return _leaderBoardService.GetLeaderBoard(match, duration);
        }
    }
}