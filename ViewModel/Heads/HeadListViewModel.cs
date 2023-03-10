using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Head_brands.web.ViewModel.Heads
{
    public class HeadListViewModel
    {   
        public int Id { get; set; }
        public string Name { get; set;}
        public string Brand { get; set; } 
        public int Volume { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}