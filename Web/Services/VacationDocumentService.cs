using Aspose.Pdf;
using Models;
using Web.Services.Interfaces;
using System;
using System.IO;
using System.Linq;

namespace Web.Services
{
    public class VacationDocumentService : IVacationDocumentService
    {   
        private readonly IUserService _userService;

        public VacationDocumentService(IUserService userService)
        {
            this._userService = userService ?? throw new ArgumentNullException(nameof(userService));;
        }

        public void GenerateDocument(User user, Vacation vacation)
        {
            var users = this._userService.GetUsers();
            var ceo = users.SingleOrDefault(x => x.Role.Name == "CEO");
            var teamLead = user.Team.TeamLeader;

            Document document = new Document();
            Page page = document.Pages.Add();
            page.Paragraphs.Add(new HtmlFragment(HtmlCode(user, ceo, teamLead, vacation)));
            document.Save("Files/document.pdf");
        }

        private string HtmlCode(User user, User ceo, User teamLead, Vacation vacation) 
        { 
            return "<!DOCTYPE html>"
                    + "<html>"
                    + "<head>"
                    + "<title>Молба</title>"
                    + "</head>"
                    + "<body>"
                    + "<style>" 
                    + "[center] { margin: auto; width: 55%; text-align: justify; }" 
                    + "[center-text] { text-align: center; }" 
                    + "[right-text] { text-align: right; }" 
                    + "[f-left] { float: left; }" 
                    + "</style>" 
                    + "<div right-text>" 
                    + $"<p><b>До: {ceo.FirstName} {ceo.LastName}</b></p>" 
                    + "<p><b>Управител</b></p>" 
                    + "</div>" 
                    + "<div center><h1 center-text>Молба</h1><h3 center-text>За отпуск</h3>" 
                    + $"<p><b>От: {user.FirstName} {user.LastName}, {(user.Role.Name == "Developer" ? "Програмист" : user.Role.Name)}</b></p><br>" 
                    + $"<p><b>Уважаеми/а г-н/г-жо {ceo.LastName},</b></p>" 
                    + "<p>Моля да ми бъде разрешен платен отпуск.</p><br>" 
                    + $"<p>От: {vacation.FromDate}</p><br><p>До: {vacation.ToDate}</p><br>" 
                    + "</div>" 
                    + "<div>" 
                    + $"<p f-left><br>Дата: {vacation.CreationDate}</p><br>"
                    + "<p right-text>С уважение: ...................(<i>подпис</i>)</p>"
                    + "</div>"
                    + "<br><hr><br>"
                    + "<div right-text>" 
                    + $"<p><b>До: {teamLead.FirstName} {teamLead.LastName}</b></p>" 
                    + "<p><b>Лидер на екип</b></p>" 
                    + "</div>" 
                    + "<div center><h1 center-text>Молба</h1><h3 center-text>За отпуск</h3>" 
                    + $"<p><b>От: {user.FirstName} {user.LastName}, {(user.Role.Name == "Developer" ? "Програмист" : user.Role.Name)}</b></p><br>" 
                    + $"<p><b>Уважаеми/а г-н/г-жо {teamLead.LastName},</b></p>" 
                    + "<p>Моля да ми бъде разрешен платен отпуск.</p><br>" 
                    + $"<p>От: {vacation.FromDate}</p><br><p>До: {vacation.ToDate}</p><br>" 
                    + "</div>" 
                    + "<div>" 
                    + $"<p f-left><br>Дата: {vacation.CreationDate}</p><br>"
                    + "<p right-text>С уважение: ...................(<i>подпис</i>)</p>"
                    + "</div>"
                    + "</body>"
                    + "</html>"; 
        }
    }
}