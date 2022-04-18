using Alquilar.DAL;
using Alquilar.Models;

namespace Alquilar.Services.Interfaces
{
    public interface ITokenService
    {
		Token GetToken();
		void SetToken(Token token);
		public Token GenerateToken(Usuario user);
	}
}
