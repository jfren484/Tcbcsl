using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Tcbcsl.Data.Entities;
using Tcbcsl.Presentation.Areas.Admin.Models;
using Tcbcsl.Presentation.Helpers;

namespace Tcbcsl.Presentation.Areas.Admin.Controllers
{
    [AuthorizeRedirect(Roles = Roles.LeagueCommissioner + ", " + Roles.TeamCoach)]
    [RouteArea("Admin")]
    [RoutePrefix("Player")]
    public class PlayerController : AdminControllerBase
    {
        #region List

        [Route("")]
        public ActionResult List()
        {
            return View(new PlayerEditModel());
        }

        //[HttpPost]
        [Route("Data")]
        public JsonResult Data()
        {
            var data = DbContext.Players
                                .ToList()
                                .FilterTeamsForUser(User, p => p.CurrentTeamId)
                                .Select(ty =>
                                {
                                    var model = Mapper.Map<PlayerEditModel>(ty);
                                    model.EditUrl = Url.Action("Edit", new { id = model.PlayerId });

                                    return model;
                                });

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create/Edit

        [Route("Create")]
        public ActionResult Create()
        {
            var model = new PlayerEditModel
                        {
                            Team = new PlayerEditTeamModel(),
                            IsActive = true
                        };
            PopulateDropdownLists(model);

            return View("Edit", model);
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(PlayerEditModel model)
        {
            var player = Mapper.Map<Player>(model);

            DbContext.Players.Add(player);
            DbContext.SaveChanges(User.Identity.Name);

            return RedirectToAction("List");
        }

        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id)
        {
            var player = DbContext.Players.SingleOrDefault(ty => ty.PlayerId == id);
            if (player == null || (!User.IsInRole(Roles.LeagueCommissioner) && !User.IsTeamIdValidForUser(player.CurrentTeamId)))
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<PlayerEditModel>(player);
            PopulateDropdownLists(model);

            return View(model);
        }

        [HttpPost]
        [Route("Edit/{id:int}")]
        public ActionResult Edit(int id, PlayerEditModel model)
        {
            var player = DbContext.Players.SingleOrDefault(ty => ty.PlayerId == id);
            if (player == null || (!User.IsInRole(Roles.LeagueCommissioner) && !User.IsTeamIdValidForUser(player.CurrentTeamId)))
            {
                return HttpNotFound();
            }

            Mapper.Map(model, player);
            DbContext.SaveChanges(User.Identity.Name);

            return RedirectToAction("List");
        }

        #endregion

        #region Helpers

        private void PopulateDropdownLists(PlayerEditModel model)
        {
            var coaches = DbContext.TeamYears
                                   .Where(ty => ty.Year == Consts.CurrentYear && !Consts.InvalidPlayerTeamIds.Contains(ty.TeamId))
                                   .OrderBy(ty => ty.DivisionYear.IsInLeague)
                                   .ThenBy(ty => ty.FullName)
                                   .ToList()
                                   .FilterTeamsForUser(User, ty => ty.TeamId)
                                   .Select(ty => new SelectListItem {Value = ty.TeamId.ToString(), Text = ty.FullName})
                                   .ToList();
            model.Team.ItemSelectList = new SelectList(coaches, "Value", "Text", model.Team.TeamId);
        }

        #endregion
    }
}