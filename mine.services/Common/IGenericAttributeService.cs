using mine.core.Domain;
using mine.core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.services.Common
{
    public interface IGenericAttributeService
    {
        /// <summary>
        /// Get attributes
        /// </summary>
        /// <param name="entityId">Entity identifier</param>
        /// <param name="keyGroup">Key group</param>
        /// <returns>Get attributes</returns>
        IList<GenericAttribute> GetAttributesForEntity(int entityId, string keyGroup);
        void SaveAttribute<T>(BaseEntity entity, string key, T value, int storeId = 0);
        /// <summary>
        /// Deletes an attribute
        /// </summary>
        /// <param name="attribute">Attribute</param>
        void DeleteAttribute(GenericAttribute attribute);
        /// <summary>
        /// update an attribute
        /// </summary>
        /// <param name="attribute"></param>
        void UpdateAttribute(GenericAttribute attribute);
        /// <summary>
        /// insert
        /// </summary>
        /// <param name="attribute"></param>
        void InsertAttribute(GenericAttribute attribute);
    }
}
