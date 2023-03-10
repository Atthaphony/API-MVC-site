using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Head_brands.web.ViewModel.Heads
{
    public class HeadsPostListModel
    {
        [Required(ErrorMessage = "Namn måste anges")]
        [DisplayName("Namn")]
        public string Name { get; set;}


        [Required(ErrorMessage = "Märke måste anges")]
        [DisplayName("Märke")]
        public string Brand { get; set; } 


        [DisplayName("Beskrivning")]
        public string Description { get; set; }
    }
}