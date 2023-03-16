using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CMS_Dashboard_v1.Models.ModelForm
{
    public class ContentModel
    {
        public ContentModel()
        {
            List = new List<ContentViewModel>();
        }
        public int content_id { get; set; }
        public int section_id { get; set; }
        public string header { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
        public bool status { get; set; }
        public List<ContentViewModel> List { get; set; }
    }




    public class ContentViewModel
    {
        public int content_id { get; set; }
        [Required(ErrorMessage = "Silakan Pilih Menu Terlebih Dulu")]
        public int menu_id { get; set; }
        [Required(ErrorMessage = "Silakan Pilih Section Terlebih Dulu")]
        public int section_id { get; set; }
        [Required(ErrorMessage = "Header Tidak Boleh Kosong")]
        [StringLength(100, ErrorMessage = "Panjang karakter harus kurang dari 100 karakter.")]
        public string header { get; set; } = string.Empty;
        [Required(ErrorMessage = "Tittle Tidak Boleh Kosong")]
        [StringLength(100, ErrorMessage = "Panjang karakter harus kurang dari 100 karakter.")]
        public string title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description Tidak Boleh Kosong")]
        public string description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Url Tidak Boleh Kosong")]
        public string url { get; set; } = string.Empty;

        [Required(ErrorMessage = "Silakan Pilih Gambar")]
        public IFormFile Photos { get; set; }
        public string imageurl { get; set; } = string.Empty;
        public string Section { get; set; } = string.Empty;
        public string Menu { get; set; } = string.Empty;
    }

    public class InsertContentModel
    {
        public int section_id { get; set; }
        public string header { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
        public bool status { get; set; }
        public string created_by { get; set; } = string.Empty;
    }

    public class PutContentModel
    {
        public int content_id { get; set; }
        public int section_id { get; set; }
        public string header { get; set; } = string.Empty;
        public string title { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public string image { get; set; } = string.Empty;
        public string url { get; set; } = string.Empty;
        public bool status { get; set; }
        public string modified_by { get; set; } = string.Empty;
    }
}
