using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sintra.Domain.Entities
{
    public partial class Category
    {
        public Category()
        {
            InverseParent = new HashSet<Category>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        
        public virtual ICollection<Category> InverseParent { get; set; }
    }
}
