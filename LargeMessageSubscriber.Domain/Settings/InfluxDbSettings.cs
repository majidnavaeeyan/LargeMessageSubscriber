﻿namespace LargeMessageSubscriber.Domain.Settings
{
  public class InfluxDbSettings : IInfluxDbSettings
  {
    public string? Token { get; set; }
    public string? Address { get; set; }
  }
}
