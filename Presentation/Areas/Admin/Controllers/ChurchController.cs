using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using MoreLinq;
using Tcbcsl.Presentation.Helpers;

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
        [HttpPost]
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

            return Json(data);
        }

        #endregion

        #region Create/Edit

        [Authorize(Roles = "League Commissioner")]
        [Route("Create")]
        public ActionResult Create()
        {
            return View("Edit", new ChurchEditModel
                                {
                                    Address = new AddressEditModel
                                            {
                                                State = new StateEditModel {States = GetStates()}
                                            },
                                    PhoneNumbers = new PhoneEditModelList
                                                    {
                                                        new PhoneEditModel {PhoneTypes = GetPhoneTypes()}
                                                    }
                                });
        }

        [Authorize(Roles = "League Commissioner")]
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(ChurchEditModel model)
        {
            var church = Mapper.Map<Church>(model);
            church.PhoneNumbers = Mapper.Map<List<ContactPhoneNumber>>(model.PhoneNumbers);

            DbContext.Churches.Add(church);
            DbContext.SaveChanges(User.Identity.Name);

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
            model.Address.State.States = GetStates();

            var phoneTypes = GetPhoneTypes();
            model.PhoneNumbers.ForEach(pn => { pn.PhoneTypes = phoneTypes; });

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

            var changes = ChangeTracker.GetChangeSets(church.PhoneNumbers,         model.PhoneNumbers.Models,
                                                      e => e.ContactPhoneNumberId, m => m.ContactPhoneNumberId);

            foreach (var pair in changes.CommonItems)
            {
                Mapper.Map(pair.RightItem, pair.LeftItem);
            }

            // TODO: Add
            // TODO: Delete

            DbContext.SaveChanges(User.Identity.Name);

            return RedirectToAction("List");
        }

        #endregion

        #region Helpers

        private List<PhoneTypeModel> GetPhoneTypes()
        {
            return DbContext.PhoneNumberTypes
                            .Select(Mapper.Map<PhoneTypeModel>)
                            .OrderBy(t => t.PhoneNumberTypeId)
                            .ToList();
        }

        private List<StateModel> GetStates()
        {
            return DbContext.States
                            .Select(Mapper.Map<StateModel>)
                            .OrderBy(s => s.Name)
                            .ToList();
        }

        #endregion
    }
}