using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CMS_Dashboard_v1.Models.ModelForm
{
    public class SectionModel
    {
        public SectionModel()
        {
            List = new List<SectionViewModel>();
        }
        public int section_id { get; set; }
        public int menu_id { get; set; }
        public string section_name { get; set; } = string.Empty;
        public int section_number { get; set; }
        public int section_approve { get; set; }
        public bool status { get; set; }

        public List<SectionViewModel> List { get; set; }
    }

    public class SectionViewModel
    {
        public int section_id { get; set; }
        [Required(ErrorMessage = "Silakan Pilih Menu Terlebih Dulu")]
        public int menu_id { get; set; }
        public string menu_name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Nama Section Tidak Boleh Kosong")]
        public string section_name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Urutan Tidak Boleh Kosong")]
        [Range(1, 10, ErrorMessage = "Harus diantara 1 dan 10 !!!")]
        public int section_number { get; set; }
        [Required(ErrorMessage = "Batas Content Tidak Boleh Kosong")]
        [Range(1, 50, ErrorMessage = "Harus diantara 1 and 50 !!!")]
        public int section_approve { get; set; }
    }

    public class InsertSectionModel
    {
        public int menu_id { get; set; }
        public string section_name { get; set; } = string.Empty;
        public int section_number { get; set; }
        public int section_approve { get; set; }
        public bool status { get; set; }
        public string created_by { get; set; } = string.Empty;

    }

    public class PutSectionModel
    {
        public int section_id { get; set; }
        public int menu_id { get; set; }
        public string section_name { get; set; } = string.Empty;
        public int section_number { get; set; }
        public int section_approve { get; set; }
        public bool status { get; set; }
        public string modified_by { get; set; } = string.Empty;
    }
}