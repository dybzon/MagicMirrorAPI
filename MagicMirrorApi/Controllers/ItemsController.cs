using System.Web.Http;
using MagicMirrorApi.Repository;
using System.Web.Http.Cors;
using MagicMirrorApi.Models;

namespace MagicMirrorApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/items")]
    public class ItemsController : ApiController
    {
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllItems()
        {
            var items = ItemRepository.GetAllItems();
            return Ok(items);
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult AddItem([FromBody]Item item)
        {
            if(item == null)
            {
                return NotFound();
            }
            // Figure out how to correctly map the received item from the request body.
            var newItemId = ItemRepository.CreateItem(item);
            return Ok(newItemId);
        }

        [HttpPost]
        [Route("complete")]
        public IHttpActionResult CompleteItem([FromBody]int id)
        {
            if (ItemRepository.CompleteItem(id))
            {
                return Ok(id);
            }
            else
            {
                return InternalServerError(new System.Exception("You Suck"));
            }
        }
    }
}
