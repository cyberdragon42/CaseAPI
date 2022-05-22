using CaseAPI.BusinessLogic.Dto;
using CaseAPI.BusinessLogic.Exceptions;
using CaseAPI.DataAccess.Context;
using CaseAPI.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseAPI.BusinessLogic.Services
{
    public class CaseService
    {
        private readonly CaseContext dbContext;
        public CaseService(CaseContext context)
        {
            dbContext = context;
        }

        public async Task<Account> FindAccountByNameAsync(string name)
        {
            var account = await dbContext.Accounts.FirstOrDefaultAsync(x => x.Name == name);
            return account;
        }

        public async Task<Contact> FindContactByEmailAsync(string email)
        {
            var contact = await dbContext.Contacts.FirstOrDefaultAsync(x => x.Email == email);
            return contact;
        }

        public async Task<Contact> CreateContactAsync(ContactDto contactDto)
        {
            var newContact = new Contact
            { Email = contactDto.Email, FirstName = contactDto.FirstName, LastName = contactDto.LastName };
            dbContext.Add(newContact);
            await dbContext.SaveChangesAsync();
            return newContact;
        }

        public async Task CreateAccountAsync(AccountDto accountDto)
        {
            var contact = await FindContactByEmailAsync(accountDto.Email);
            if (contact == null)
            {
                var contactDto = new ContactDto 
                { Email = accountDto.Email, FirstName = accountDto.FirstName, LastName = accountDto.LastName };
                contact = await CreateContactAsync(contactDto);
            }
            else
            {
                contact.FirstName = accountDto.FirstName;
                contact.LastName = accountDto.LastName;
            }

            dbContext.Accounts.Add(new Account
            {
                Name = accountDto.AccountName,
                ContactId = contact.Id
            });
            await dbContext.SaveChangesAsync();
        }

        public async Task CreateCaseAsync(CaseDto caseDto)
        {
            var account = await FindAccountByNameAsync(caseDto.AccountName);
            if (account == null)
                throw new NotFoundException("This account does not exist");
            var contact = await FindContactByEmailAsync(caseDto.Email);
            if (contact == null)
            {
                var contactDto = new ContactDto 
                { Email = caseDto.Email, FirstName = caseDto.FirstName, LastName = caseDto.LastName };
                contact = await CreateContactAsync(contactDto);
                account.ContactId = contact.Id;
                dbContext.Incidents.Add(new Incident
                { Description = caseDto.Description, AccountId = account.Id });
                await dbContext.SaveChangesAsync();
            }
            else
            {
                contact.FirstName = caseDto.FirstName;
                contact.LastName = caseDto.LastName;
                account.ContactId = contact.Id;
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
