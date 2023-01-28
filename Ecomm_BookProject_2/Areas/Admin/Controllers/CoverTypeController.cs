using Dapper;
using Ecomm_BookProject_2_DataAccess.Repository.IRepository;
using Ecomm_BookProject_2_Models.Models;
using Ecomm_BookProject_2_Utility;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecomm_BookProject_2.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            CoverType coverType = new CoverType();
            if (id == null)
                // this is for Create Code
                return View(coverType);

            // this is for Update code
            // coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());

            // this code for Update database data by StoreProcedure....
            var param = new DynamicParameters();
            param.Add("@Id", id.GetValueOrDefault());
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_GetCoverType, param);

            if (coverType == null) return NotFound();
            return View(coverType);
        }
        [HttpPost]
        public IActionResult Upsert(CoverType coverType)
        {
            if (coverType == null) return NotFound();
            if (!ModelState.IsValid) return View(coverType);

            // this line of code using storeProcedure....
            var param = new DynamicParameters();
            param.Add("@Name", coverType.Name);

            if (coverType.Id == 0)
                //_unitOfWork.CoverType.Add(coverType);

                // this line of code using StoreProcedure for save data into database....
                _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, param);
            else
            {
                // _unitOfWork.CoverType.Update(coverType);

                // this line of code StoreProcedure for update data into database
                param.Add("@id", coverType.Id);
                _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, param);
            }
            // _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        #region APIs
        [HttpGet]
        public IActionResult GetAll()
        {
            //var covertypeList = _unitOfWork.CoverType.GetAll();
            //return Json(new { data = covertypeList });

            //  return Json(new { data = _unitOfWork.CoverType.GetAll() });

            return Json(new { data = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_GetCoverTypes) });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var coverTypeList = _unitOfWork.CoverType.Get(id);
            if (coverTypeList == null)
                return Json(new { success = false, message = "Error While Delete Data !!" });
            // This line of code by StoreProcedure for delete data into database...
            var param = new DynamicParameters();
            param.Add("@Id", id);
            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, param);
            return Json(new { success = true, message = "Data Delete Successfully...." });

            //_unitOfWork.CoverType.Remove(coverTypeList);
            //_unitOfWork.Save();
            //return Json(new { success = true, message = "Data Delete Successfully...." });
        }
        #endregion
    }
}
