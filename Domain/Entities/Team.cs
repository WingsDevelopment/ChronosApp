using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Team
    {
        public int Id { get; set; }

        public int ProjectId { get; set; }

        [Computed]
        public Project Project { get; set; }

        public string TeamName { get; set; }

        public bool IsDeleted { get; set; }

        public Team()
        {

        }

        public Team(int projectId, string teamName)
        {
            ProjectId = projectId;
            TeamName = teamName;
        }
    }
}
