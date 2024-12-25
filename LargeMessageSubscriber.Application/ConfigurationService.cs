using LargeMessageSubscriber.Domain;
using LargeMessageSubscriber.Domain.Enums;
using LargeMessageSubscriber.Domain.Services;
using LargeMessageSubscriber.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace LargeMessageSubscriber.Application
{
  public class ConfigurationService : IConfigurationService
  {
    public IEnumerable<ErrorWarningModel> GetAllErrorTypes()
    {
      var result = new List<ErrorWarningModel>();

      var items = Enum.GetValues(typeof(ErrorTypes));

      foreach (var item in items)
      {
        var data = new ErrorWarningModel { EnglishValue = item.ToString(), Id = item.GetHashCode(), PersianValue = ((DescriptionAttribute)item?.GetType()?.GetMember(item.ToString())?.First()?.GetCustomAttribute(typeof(DescriptionAttribute), false)).Description };
        result.Add(data);
      }

      return result;
    }

    public IEnumerable<ErrorWarningModel> GetAllWarningTypes()
    {
      var result = new List<ErrorWarningModel>();

      var items = Enum.GetValues(typeof(WarningTypes));

      foreach (var item in items)
      {
        var data = new ErrorWarningModel { EnglishValue = item.ToString(), Id = item.GetHashCode(), PersianValue = ((DescriptionAttribute)item.GetType().GetMember(item.ToString()).First().GetCustomAttribute(typeof(DescriptionAttribute), false)).Description };
        result.Add(data);
      }

      return result;
    }

    public string GenerateJwtToken(TokenInputModel model)
    {
      var (validationResult, errors, warnings) = GenerateJwtTokenValidation(model);
      if (!validationResult)
        throw new ValidationException(errors, warnings);



      var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecretKey12YourSuperSecretKey12YourSuperSecretKey12YourSuperSecretKey12YourSuperSecretKey12YourSuperSecretKey12"));
      var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

      var claims = new[] { new Claim(JwtRegisteredClaimNames.Sub, model.Username) };

      var tokenDescriptor = new JwtSecurityToken(issuer: "LargeMessageSubscriber", claims: claims, expires: DateTime.UtcNow.AddHours(1), signingCredentials: signingCredentials);
      var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

      return token;
    }

    public (bool, IEnumerable<int>, IEnumerable<int>) ValidateToken(string token)
    {
      var result = true;
      var errors = new List<int>();
      var warnings = new List<int>();



      var tokenHandler = new JwtSecurityTokenHandler();

      //Number : 109
      if (!tokenHandler.CanReadToken(token) || string.IsNullOrWhiteSpace(token))
        return (false, new List<int> { (int)ErrorTypes.InvalidInputToken }, new List<int>());


      var securityToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

      var issueDate = securityToken.Claims.Where(q => q.Type == "exp").FirstOrDefault()?.Value;
      var username = securityToken.Claims.Where(q => q.Type == "sub").FirstOrDefault()?.Value;
      var issuerName = securityToken.Claims.Where(q => q.Type == "iss").FirstOrDefault()?.Value;


      //Number : 106
      if (issuerName != "LargeMessageSubscriber")
        errors.Add((int)ErrorTypes.TokenIssuerNameIsNotValid);

      //Number : 105
      var validUsernames = new Dictionary<string, string> { { "majid", "12345678" } };
      if (!validUsernames.ContainsKey(username))
        errors.Add((int)ErrorTypes.UsernameIsNotValid);

      //Number : 107
      var expirationTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(issueDate)).UtcDateTime;
      if ((DateTime.UtcNow - expirationTime).Hours >= 1)
        errors.Add((int)ErrorTypes.TokenHasExpired);



      ////////////////////////////////////////
      if (errors.Count > 0)
        result = false;

      return (result, errors, warnings);
      ////////////////////////////////////////
    }

    private (bool, IEnumerable<int>, IEnumerable<int>) GenerateJwtTokenValidation(TokenInputModel model)
    {
      var result = true;
      var errors = new List<int>();
      var warnings = new List<int>();




      //Number : 103
      if (string.IsNullOrWhiteSpace(model.Username))
        errors.Add((int)ErrorTypes.UsernameIsNull);

      //Number : 104
      if (string.IsNullOrWhiteSpace(model.Password))
        errors.Add((int)ErrorTypes.PasswordIsNull);

      //Number : 105
      var validUsernames = new List<string>() { "majid" };
      if (!string.IsNullOrWhiteSpace(model.Username) && !validUsernames.Contains(model.Username))
        errors.Add((int)ErrorTypes.UsernameIsNotValidInDatabase);




      ////////////////////////////////////////
      if (errors.Count > 0)
        result = false;

      return (result, errors, warnings);
      ////////////////////////////////////////
    }
  }
}