﻿using ESale.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESale.Entities.Concrete
{
    public class Cart : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }


        public List<CartItem> CartItems { get; set; }
    }
}
