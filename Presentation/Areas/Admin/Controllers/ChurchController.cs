using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [Authorize(Roles = "League Commissioner, Team Coach")]
    [RouteArea("Admin")]
    [RoutePrefix("Church")]
    public class ChurchController : AdminControllerBase
    {
        #region List

        [Authorize(Roles = "League Commissioner")]
        [Route("")]
        public ActionResult List()
        {
            return View();
        }

        [Authorize(Roles = "League Commissioner")]
        //[HttpPost]
        [Route("Data")]
        public JsonResult Data()
        {
            var data = DbContext.Churches
                                .OrderByDescending(c => c.Created)
                                .ToList()
                                .Select(n =>
                                        {
                                            var model = Mapper.Map<ChurchEditModel>(n);
                                            model.EditUrl = Url.Action("Edit", new {id = model.ChurchId});

                                            return model;
                                        });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create/Edit

        [Authorize(Roles = "League Commissioner")]
        [Route("Create")]
        public ActionResult Create()
        {
            return View("Edit", new ChurchEditModel
                                {
                                    ContactInfo = new ContactInfoEditModel
                                                  {
                                                      Address = new AddressEditModel
                                                                {
                                                                    State = new StateEditModel {States = GetStates()}
                                                                },
                                                      PhoneNumbers = new List<PhoneEditModel>
                                                                     {
                                                                         new PhoneEditModel {PhoneNumberTypeId = PhoneNumberType.Main}
                                                                     }
                                                  }
                                });
        }

        [Authorize(Roles = "League Commissioner")]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(ChurchEditModel model)
        {
            var church = Mapper.Map<Church>(model);
            UpdateCreatedFields(church);
            DbContext.Churches.Add(church);
            DbContext.SaveChanges();

            return RedirectToAction("List");
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var church = DbContext.Churches.SingleOrDefault(c => c.ChurchId == id);
            if (church == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<ChurchEditModel>(church);
            model.ContactInfo.Address.State.States = GetStates();
            // TODO: fix this
            model.ContactInfo.PhoneNumbers[0].PhoneNumberTypeId = model.ContactInfo.PhoneNumbers[0].PhoneNumberTypeId ?? PhoneNumberType.Main;

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, ChurchEditModel model)
        {
            var church = DbContext.Churches.SingleOrDefault(c => c.ChurchId == id);
            if (church == null)
            {
                return HttpNotFound();
            }

            Mapper.Map(model, church);
            UpdateModifiedFields(church);
            DbContext.SaveChanges();

            return RedirectToAction("List");
        }

        #endregion

        #region Helpers

        private List<StateModel> GetStates()
        {
            return DbContext.States
                            .Select(s => new StateModel { StateId = s.StateId, StateName = s.Name })
                            .OrderBy(s => s.StateName)
                            .ToList();
        }

        #endregion
    }
}