﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRM_Models
{
    public class Class1
    {
        public void main()
        { 
            DatabaseEntities data = new DatabaseEntities();
            data.SaveChanges();
        }
    }
}