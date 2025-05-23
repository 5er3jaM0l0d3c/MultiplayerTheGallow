﻿using API.Interfaces;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private IPlayer Player { get; set; }

        public PlayerController(IPlayer player)
        {
            Player = player;
        }

        [HttpGet("GetPlayers")]
        public List<Player> GetPlayers()
        {
            return Player.GetPlayers();
        }

        [HttpGet("GetPlayerId")]
        public Player GetPlayer(int id)
        {
            return Player.GetPlayer(id);
        }

        [HttpGet("GetPlayerLP")]
        public Player GetPlayer(string Login, string Password)
        {
            return Player.GetPlayer(Login, Password);
        }

        [HttpGet("AutorizePlayer")]
        public Player AutorizePlayer(string Login, string Password)
        {
            return Player.AutorizePlayer(Login, Password);
        }

        [HttpPost("AddPlayer")]
        public void AddPlayer([FromBody]Player player)
        {
            Player.AddPlayer(player);
        }

        [HttpPut("UpdatePlayer")]
        public void UpdatePlayer([FromBody] Player player)
        {
            Player.UpdatePlayer(player);
        }

        [HttpDelete("DeletePlayer")]
        public void DeletePlayer(int id)
        {
            Player.DeletePlayer(id);
        }

        [HttpGet("CheckConnection")]
        public void CheckConnection()
        {
            return;
        }
    }
}
