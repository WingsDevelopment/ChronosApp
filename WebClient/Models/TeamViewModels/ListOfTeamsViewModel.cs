using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.TeamViewModels
{
    public class ListOfTeamsViewModel
    {
        public IEnumerable<Team> Teams { get; set; }

        public ListOfTeamsViewModel()
        {

        }

        public ListOfTeamsViewModel(IEnumerable<Team> teams)
        {
            Teams = teams;
        }
    }
}
