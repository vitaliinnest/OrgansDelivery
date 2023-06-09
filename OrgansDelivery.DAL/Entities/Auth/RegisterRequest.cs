﻿namespace OrganStorage.DAL.Entities.Auth;

public class RegisterRequest
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string RepeatPassword { get; set; }
	public Guid? InviteCode { get; set; }
}
