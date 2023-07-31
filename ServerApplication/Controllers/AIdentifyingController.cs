using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace ServerApplication.Controllers;

public abstract class AIdentifyingController : ControllerBase
{
    protected void IdentifyUserId(Guid userId)
    {
        using var claims = HttpContext.User.Claims.GetEnumerator();
        while (claims.MoveNext())
        {
            var claim = claims.Current;
            if (!claim.Type.Equals(ClaimTypes.NameIdentifier)) continue;
            var id = Guid.Parse(claim.Value);
            if (!userId.Equals(id))
                throw new ArgumentException("Wrong user id");
            break;
        }
    }
}