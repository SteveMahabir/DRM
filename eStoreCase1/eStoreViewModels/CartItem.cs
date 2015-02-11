using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStoreViewModels
{

    /// <summary>
    /// CartItem - simple container object to hold 1 item of the catalogue
    /// 
    /// Revisions   DD/MM/YY    Description
    /// ----------  --------    -----------
    /// S. Mahabir  22/05/14    Initial Code
    /// </summary>
    public class CartItem
    {
        // 
        //  Data Members
        //
        public int Qty { get; set; }
        public string ProdCd { get; set; }
        public string ProdName { get; set; }
        public string Description { get; set; }
        public string Graphic { get; set; }
        public decimal Msrp { get; set; }
    }
}
