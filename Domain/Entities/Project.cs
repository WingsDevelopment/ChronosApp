using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Project
    {
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Computed]
        public Client Client { get; set; }

        public string ProjectName { get; set; }

        public bool IsDeleted { get; set; }

        public Project()
        {

        }

        public Project(int clientId, string projectName)
        {
            ClientId = clientId;
            ProjectName = projectName;
        }
    }
}
