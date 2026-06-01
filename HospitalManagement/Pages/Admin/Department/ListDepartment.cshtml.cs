using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagement.Pages.Admin.Department
{
    public class ListDepartmentModel : PageModel
    {

        [BindProperty]

        public List<DepartmentTbl> DepartmentTblList { get; set; }

        private readonly IDepartmentTblServices DepartmentTblObj;

        public ListDepartmentModel(IDepartmentTblServices DepartmentTblObj)
        {
            this.DepartmentTblObj = DepartmentTblObj;
        }
        public void OnGet()
        {
            DepartmentTblList = DepartmentTblObj.AllDepartment();
        }

        public IActionResult OnPost(int DepartmentId)
        {
            int Delete = DepartmentTblObj.Delete(DepartmentId);

            if (Delete == 1)
            {
                TempData["MsgSuccess"] =
                    "Department Deleted Successfully";

                return RedirectToPage();
            }
            else
            {
                TempData["MsgNormal"] ="Department Not Found";

                return RedirectToPage();
            }
        }

       
    }
}
