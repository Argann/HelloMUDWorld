﻿namespace HelloMUDWorld.Data.Identity;

public class TokenProvider
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public string? XsrfToken { get; set; }
}