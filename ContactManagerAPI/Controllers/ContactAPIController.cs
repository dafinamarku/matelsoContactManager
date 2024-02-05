
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using ModelLayer.DB;
using ModelLayer.Migrations;
using RepositoryLayer;
using System.Net;

namespace ContactManagerAPI.Controllers
{
    [ApiController]
    public class ContactAPIController : ControllerBase
    {
        private ContactRepository contactRepository;
        private LogRepository logRepository;

        public ContactAPIController(DataContext dataContext)
        {
            contactRepository = new ContactRepository(dataContext);
            logRepository = new LogRepository(dataContext);
        }
        // GET: api/<ContactAPI>
        [HttpGet]
        [Route("api/[controller]/GetAll")]
        public IActionResult Get()
        {
            try
            {
                List<Contact> allContacts = contactRepository.GetAll();
                if(!allContacts.Any())
                    return NotFound(new Result<string>(null, ResponseType.NotFound));

                return Ok(new Result<List<Contact>>(allContacts, ResponseType.Success));
            }
            catch (Exception ex)
            {
                logRepository.RegisterLog(ex.Message, ActionType.Error);
                return BadRequest(new Result<List<Contact>>(null, ex)); 
            }
        }

        // GET api/<ContactAPI>/5
        [HttpGet]
        [Route("api/[controller]/GetById/{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                Contact contact = contactRepository.Get(id);
                if (contact == null)
                    return NotFound(new Result<string>(null, ResponseType.NotFound));

                return Ok(new Result<Contact>(contact, ResponseType.Success));
            }
            catch (Exception ex)
            {
                logRepository.RegisterLog(ex.Message, ActionType.Error);
                return BadRequest(new Result<Contact>(null, ex));
            }
        }

        // POST api/<ContactAPI>
        [HttpPost]
        [Route("api/[controller]/Save")]
        public IActionResult Post([FromBody] Contact newContact)
        {
            try
            {
                if(contactRepository.Get(newContact.Id) != null)
                    return StatusCode((int)HttpStatusCode.Forbidden, (new Result<string>($"Contct with Id: {newContact.Id} already exists.", ResponseType.Failure)));

                if (!contactRepository.EmailExist(newContact.Id, newContact.Email))
                {
                    contactRepository.Save(newContact);
                    return Ok(new Result<Contact>(newContact, ResponseType.Success));
                }
                else
                    return StatusCode((int)HttpStatusCode.Forbidden, (new Result<string>($"{newContact.Email} already exists.", ResponseType.Failure)));
            }
            catch (Exception ex)
            {
                logRepository.RegisterLog(ex.Message, ActionType.Error);
                return BadRequest(new Result<Contact>(null, ex));
            }
        }

        // PUT api/<ContactAPI>/5
        [HttpPut]
        [Route("api/[controller]/Update")]
        public IActionResult Put([FromBody] Contact contactToUpdate)
        {
            try
            {
                if(contactRepository.Get(contactToUpdate.Id) == null)
                    return NotFound(new Result<string>(null, ResponseType.NotFound));
                
                if (!contactRepository.EmailExist(contactToUpdate.Id, contactToUpdate.Email))
                {
                    Contact storedContact = contactRepository.Update(contactToUpdate);
                    return Ok(new Result<Contact>(storedContact, ResponseType.Success));
                }
                else
                    return StatusCode((int)HttpStatusCode.Forbidden, (new Result<string>($"{contactToUpdate.Email} already exists.", ResponseType.Failure)));
            }
            catch (Exception ex)
            {
                logRepository.RegisterLog(ex.Message, ActionType.Error);
                return BadRequest(new Result<Contact>(null, ex));
            }
        }

        // DELETE api/<ContactAPI>/5
        [HttpDelete]
        [Route("api/[controller]/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (contactRepository.Get(id) == null)
                    return NotFound(new Result<string>(null, ResponseType.NotFound));
                
                contactRepository.Delete(id);
                return Ok(new Result<string>("Deleted Successfully", ResponseType.Success));
            }
            catch (Exception ex)
            {
                logRepository.RegisterLog(ex.Message, ActionType.Error);
                return BadRequest(new Result<string>(null, ex));
            }
        }
    }
}
