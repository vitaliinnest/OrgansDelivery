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
public class OrganController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public OrganController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpGet("{tenantId}")]
    public ActionResult<List<Tenant>> GetOrgans(Guid tenantId)
    {
        var organs = _appDbContext.Organs
            .IgnoreQueryFilters()
            .Where(o => o.TenantId == tenantId)
            .ToList();

        return Ok(organs);
    }
}
