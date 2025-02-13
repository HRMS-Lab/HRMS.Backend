using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;

namespace HRMS.Presentation.Authorization
{
	public class JwtDecoder
	{
		private readonly string _secretKey;

		public JwtDecoder(string secretKey)
		{
			_secretKey = secretKey;
		}

		public UserClaims DecodeJwt(string token)
		{
			var handler = new JwtSecurityTokenHandler();

			if (handler.CanReadToken(token))
			{
				var jwtToken = handler.ReadJwtToken(token);

				var userClaims = new UserClaims
				{
					Id = int.Parse(jwtToken.Claims.FirstOrDefault(c => c.Type == "Id")?.Value),
					Username = jwtToken.Claims.FirstOrDefault(c => c.Type == "Username")?.Value,
					OrganizationID = jwtToken.Claims.FirstOrDefault(c => c.Type == "OrganizationID")?.Value,
					OrganizationName = jwtToken.Claims.FirstOrDefault(c => c.Type == "OrganizationName")?.Value,
					AllowedPages = JsonConvert.DeserializeObject<int[]>(jwtToken.Claims.FirstOrDefault(c => c.Type == "AllowedPages")?.Value)
				};

				return userClaims;
			}
			else
			{
				throw new Exception("Invalid JWT token");
			}
		}
	}
}
