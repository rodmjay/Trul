using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trul.Infrastructure.Crosscutting.Validator;

namespace Trul.Application.DTO
{
    [Serializable]
    public class MenuDTO : BaseObjectDTO, IEntityValidator
    {
        /// <summary>
        /// 
        /// </summary>
        public string ApplicationCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LinkName { get; set; }

        public bool IsValid<TEntity>(TEntity item) where TEntity : class {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetInvalidMessages<TEntity>(TEntity item) where TEntity : class {
            throw new NotImplementedException();
        }
    }
}
