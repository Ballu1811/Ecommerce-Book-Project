using Ecomm_BookProject_2.DataAccess.Data;
using Ecomm_BookProject_2_DataAccess.Repository.IRepository;
using Ecomm_BookProject_2_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm_BookProject_2_DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context) :base(context)
        {
            _context = context;    
        }
        public void Update(Category category)
        {
            _context.Update(category);
        }
    }
}
