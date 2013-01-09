

/*
	Automatik generate edilen service class. Burada değişiklik yapmayın!
*/

using System;
using Trul.Application.DTO;
using Trul.Domain.Entities;
using Trul.Domain.Repositories;
using Trul.Service.Core;

namespace Trul.Service
{
    /// <summary>
    /// PersonService
    /// </summary>
    public partial class PersonService : RepositoryService<Person, PersonDTO, Int32>, IPersonService
    {
        public PersonService(IPersonRepository repository)
            : base(repository)
        {

        }
    }
}
