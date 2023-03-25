﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrganStorage.DAL.Consts;
using OrganStorage.DAL.Data;
using OrganStorage.DAL.Entities;

namespace OrganStorage.Web.Admin.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = UserRoles.ADMIN)]
public class TenantController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public TenantController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("all")]
    public ActionResult<List<Tenant>> GetAllTenants()
    {
        var tenants = _appDbContext.Tenants.IgnoreQueryFilters().ToList();
        return Ok(tenants);
    }
}
