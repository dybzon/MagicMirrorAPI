using MagicMirrorApi.Repository;
using System.Web.Http;

namespace MagicMirrorApi.Controllers
{
    [RoutePrefix("api/mirror")]
    public class MirrorController : ApiController
    {
        [HttpGet]
        [Route("shutdown")]
        public IHttpActionResult ShutDownMirror()
        {
            return Ok(MirrorRepository.ShutDownMirror());
        }

        [HttpGet]
        [Route("shouldshutdown")]
        public IHttpActionResult ShouldShutDownMirror()
        {
            return Ok(MirrorRepository.ShouldShutDownMirror());
        }
    }
}
