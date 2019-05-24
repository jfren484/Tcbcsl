using AutoMapper;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Tcbcsl.Data;
using Tcbcsl.Presentation.Models;

namespace Tcbcsl.Presentation.Controllers
{
    public class CoachController : TcbcslControllerBase
    {
        public CoachController(TcbcslDbContext dbContext) : base(dbContext) { }

        [Route("Coaches/{year:year?}")]
        public ActionResult Coaches(int year = Consts.CurrentYear)
        {
            var data = DbContext
                .DivisionYears
                .Where(dy => dy.Year == year && dy.IsInLeague)
                .Select(dy => new
                              {
                                  DivisionName = dy.Name,
                                  ConferenceSort = dy.ConferenceYear.Sort,
                                  DivisionSort = dy.Sort,
                                  Teams = dy.TeamYears
                                            .Select(ty => new
                                                          {
                                                              TeamId = ty.TeamId,
                                                              Year = year,
                                                              TeamName = ty.FullName,
                                                              HeadCoach = ty.HeadCoach
                                                          })
                                            .ToList()
                              })
                .ToList();

            var divisions = data
                .OrderBy(d => d.ConferenceSort)
                .ThenBy(d => d.DivisionSort)
                .Select(d => new CoachListDivisionModel
                              {
                                  DivisionName = d.DivisionName,
                                  Teams = d.Teams
                                            .Select(ty => new CoachListTeamModel
                                                          {
                                                              TeamId = ty.TeamId,
                                                              Year = year,
                                                              TeamName = ty.TeamName,
                                                              Coach = new CoachListCoachModel
                                                                      {
                                                                          Name = $"{ty.HeadCoach.FirstName} {ty.HeadCoach.LastName}",
                                                                          Comments = string.IsNullOrWhiteSpace(ty.HeadCoach.Comments)
                                                                                         ? null
                                                                                         : new HtmlString(ty.HeadCoach.Comments),
                                                                          ContactInfo = Mapper.Map<ContactInfoModel>(ty.HeadCoach)
                                                              }
                                                          })
                                            .ToList()
                              })
                .ToList();

            return View(new CoachListModel
                        {
                            Year = year,
                            Divisions = divisions
                        });
        }
    }
}