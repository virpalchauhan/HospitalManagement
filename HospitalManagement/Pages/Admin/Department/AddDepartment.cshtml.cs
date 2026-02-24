using HospitalManagement.Entity.Model;
using HospitalManagement.Services;
using HospitalManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HospitalManagement.Pages.Admin.Department
{
    public class AddDepartmentModel : PageModel
    {


        private readonly IDepartmentTblServices DepartmentTblObj;

        [BindProperty]
        public DepartmentTblView DepartmentTblView { get; set; } = new DepartmentTblView();


        [BindProperty]
        public string? DepartmentName { get; set; }



        public AddDepartmentModel(IDepartmentTblServices DepartmentTblObj)
        {
            this.DepartmentTblObj = DepartmentTblObj;
        }



        public void OnGet(int id)
        {


            if (id > 0)
            {
                var DepartmentTblData = DepartmentTblObj.SingleDepartment(id);

                if (DepartmentTblData != null)
                {
                    DepartmentTblView.DepartmentName = DepartmentTblData.DepartmentName;
                    DepartmentTblView.Description = DepartmentTblData.Description;
                    DepartmentTblView.IsActive = DepartmentTblData.IsActive;
                }
            }





        }

        public IActionResult OnPost(int id)
        {


            if (ModelState.IsValid)
            {

                if (id>0)
                {
                    DepartmentTbl UpdateDepartmentTbl = new DepartmentTbl()
                    {
                        DepartmentName = DepartmentTblView.DepartmentName,
                        Description = DepartmentTblView.Description,
                        IsActive = DepartmentTblView.IsActive ?? 0

                    };

                    int UpdateValue = DepartmentTblObj.Update(UpdateDepartmentTbl, id);


                    if (UpdateValue == 1)
                    {
                        TempData["Msg"] = "Department Update successfully";
                        TempData["ClearForm"] = true;
                        return RedirectToPage();
                    }
                    else if (UpdateValue == 2)
                    {
                        TempData["Msg"] = "Department already exists.";
                        TempData["ClearForm"] = true;
                        return RedirectToPage();
                    }
                }
                DepartmentTbl AddDepartmentTbl = new DepartmentTbl()
                {
                    DepartmentName = DepartmentTblView.DepartmentName,
                    Description = DepartmentTblView.Description,
                    IsActive = DepartmentTblView.IsActive ?? 0,
                    CreationDate = DateTime.Now
                };


            int ResultValue = DepartmentTblObj.AddDepartment(AddDepartmentTbl);

                if (ResultValue==1)
                {
                    TempData["Msg"] = "Department added successfully";
                    TempData["ClearForm"] = true;
                    return RedirectToPage();
                }
                else if (ResultValue==2)
                {
                    TempData["Msg"] = "Department already exists.";
                    TempData["ClearForm"] = true;
                    return RedirectToPage();
                }

                return Page();

            }

            return Page();


        }
    }
}
