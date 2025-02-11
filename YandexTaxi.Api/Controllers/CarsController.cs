﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using YandexTaxi.Application.Interfaces;
using YandexTaxi.Domain.DTOs;
using YandexTaxi.Domain.Entities;

namespace YandexTaxi.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarService _service;
        private readonly IMemoryCache _memoryCache;
        public CarsController(ICarService service, IMemoryCache memoryCache)
        {
            _service = service;
            _memoryCache = memoryCache;
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetAllCars()
        {
            var value = _memoryCache.Get("key");
            if (value == null)
            {
                _memoryCache.Set(
                    key: "key",
                    value: await _service.GetAllAsync());
            }
            return Ok(_memoryCache.Get("key") as List<Car>);
        }
        [HttpPost]
        public async ValueTask<IActionResult> CreateCarAsync(CarDTO car)
        {
            if (await _service.CreateCarAsync(car))
            {
                return Ok("Added");
            }
            return BadRequest("Error!");
        }
        [HttpGet]
        public async ValueTask<IActionResult> GetCarById(int id)
        {
            return Ok(await _service.GetCarById(id));
        }
        [HttpDelete]
        public async ValueTask<IActionResult> DeleteCarById(int id)
        {
            if (await _service.DeleteCarAsync(id))
            {
                return Ok("Deleted!");
            }
            return BadRequest("Error!");
        }
        [HttpPut]
        public async ValueTask<IActionResult> UpdateCarAsync(int id, CarDTO car)
        {
            if (await _service.UpdateCarAsync(id, car))
            {
                return Ok("updated");
            }
            return BadRequest("Error!");
        }
    }
}
