using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DataAccess
{
    public class CategoryDataAccess
    {
        /// <summary>
        /// Get's the all categories in DB.
        /// </summary>
        /// <returns></returns>
        public ICollection<CategoryDTO> GetAllCategories()
        {
            using (var context = new OplatyEntities())
            {
                var query = context.Kategorie
                    .AsEnumerable()
                    .Select(x => new CategoryDTO
                    {
                        ID = x.KategoriaID,
                        Name = x.Nazwa,
                        Opis = x.Opis
                    });

                return query.ToList();
            }
        }
    }
}
