using ESale.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Entities.Concrete
{
    public class ProductCategory : IEntity
    {

        //  Id yazmadık ama database de bir tabloya karşılık gelmesi için, 2 id de fluent api ile primary key olarak tanımlanmalı ama aynı kombinasyon olmamalı 
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
