// RechargeController.cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using dotnetapp.Models;
using dotnetapp.Repositories;
using dotnetapp.Services;
using dotnetapp.Data;
using Microsoft.AspNetCore.Authorization;

[Route("api/")]
[ApiController]
public class RechargeController : ControllerBase
{
    private readonly IRechargeService _rechargeService;

    public RechargeController(IRechargeService rechargeService)
    {
        _rechargeService = rechargeService;
    }

    [Authorize(Roles = "customer")]
    [HttpPost("addRecharge")]
    public IActionResult AddRecharge([FromBody] Recharge recharge)
    {
        var addedRecharge = _rechargeService.AddRecharge(recharge);
        return Ok(addedRecharge);
    }

    [Authorize(Roles = "admin,customer")]
    [HttpGet("getRecharge/{rechargeId}")]
    public IActionResult GetRechargeById(long rechargeId)
    {
        var recharge = _rechargeService.GetRechargeById(rechargeId);

        if (recharge == null)
        {
            return NotFound("Recharge not found");
        }

        return Ok(recharge);
    }

    [Authorize(Roles = "admin,customer")]
    [HttpGet("getRechargesByUser/{userId}")]
    public IActionResult GetRechargesByUserId(long userId)
    {
        var recharges = _rechargeService.GetRechargesByUserId(userId);
        return Ok(recharges);
    }

    [Authorize(Roles = "admin,customer")]
    [HttpGet("getAllRecharges")]
    public IActionResult GetAllRecharges()
    {
        var recharges = _rechargeService.GetAllRecharges();
        return Ok(recharges);
    }

    [Authorize(Roles = "admin,customer")]
    [HttpPost("getPricesByUser/{userId}")]
    public IActionResult GetPricesByUserId(long userId)
    {
        var prices = _rechargeService.GetPricesByUserId(userId);

        if (prices == null || prices.Count == 0)
        {
            return NotFound("No prices found for the user");
        }

        return Ok(prices);
    }

}
