﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OLX.Application.Interfaces;
using OLX.Domain.DTOs;
using OLX.Domain.Entities;

namespace OLX.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardService _service;
        private readonly IMemoryCache _memoryCache;
        public CardsController(ICardService service, IMemoryCache memoryCache)
        {
            _service = service;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetAllCards()
        {
            var value = _memoryCache.Get("key");
            if (value == null)
            {
                _memoryCache.Set(
                    key: "key",
                    value: await _service.GetAllCardAsync());
            }
            return Ok(_memoryCache.Get("key") as List<Card>);
        }
        [HttpPost]
        public async ValueTask<IActionResult> CreateCardAsync(CardsDTO card)
        {
            if (await _service.CreateCardAsync(card))
            {
                return Ok("Added");
            }
            return BadRequest("Error!");
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetCardById(int id)
        {
            return Ok(await _service.GetCardById(id));
        }
        [HttpDelete]
        public async ValueTask<IActionResult> DeleteCardById(int id)
        {
            if (await _service.DeleteCardAsync(id))
            {
                return Ok("Deleted!");
            }
            return BadRequest("Error!");
        }
        [HttpPut]
        public async ValueTask<IActionResult> UpdateCardAsync(int id,CardsDTO card)
        {
            if (await _service.UpdateCardAsync(id,card))
            {
                return Ok("updated");
            }
            return BadRequest("Error!");
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetCardAmount(int id)
        {
            var result = await _service.GetCardById(id);
            return Ok(result.Amount);
        }
    }
}
