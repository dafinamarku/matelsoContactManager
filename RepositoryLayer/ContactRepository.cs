using ModelLayer;
using ModelLayer.DB;

namespace RepositoryLayer
{
    public class ContactRepository
    {
        private DataContext _context;
        public ContactRepository(DataContext context)
        {
            _context = context;
        }

        public List<Contact> GetAll()
        {
            return _context.Contacts.ToList();
        }

        public Contact Get(Guid id)
        {
            return _context.Contacts.FirstOrDefault(x => x.Id == id);
        }

        public Contact Save(Contact contact)
        {
            var result =_context.Contacts.Add(contact);
            _context.SaveChanges();
            return result.Entity;
        }

        public Contact Update(Contact contact)
        {
            Contact contactToUpdate = _context.Contacts.FirstOrDefault(x => x.Id == contact.Id);
            if (contactToUpdate != null)
            {
                contactToUpdate.Firstname = contact.Firstname;
                contactToUpdate.Lastname = contact.Lastname;
                contactToUpdate.Email = contact.Email;
                contactToUpdate.Phonenumber = contact.Phonenumber;
                contactToUpdate.Displayname = contact.Displayname;
                contactToUpdate.Salutation = contact.Salutation;
                contactToUpdate.LastChangeTimestamp = DateTime.UtcNow;
                contactToUpdate.Birthdate = contact.Birthdate;
                contact = contactToUpdate;
            }
            _context.SaveChanges();
            return contact;
        }

        public void Delete(Guid id)
        {
            Contact contactToDelete = _context.Contacts.FirstOrDefault(x => x.Id == id);
            if(contactToDelete != null)
            {
                _context.Contacts.Remove(contactToDelete);
                _context.SaveChanges();
            }
        }

        public bool EmailExist(Guid id, string email)
        {
            return _context.Contacts.Any(x => x.Id != id && x.Email == email);
        }
    }
}