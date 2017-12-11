using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace WilliamsWeb1.Models
{
    public class Project
    {
        public string Name { get; set; }
        public string UserID { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string ProjectName { get; set; }
        private string _projectType;
        public string ProjectType
        {
            get { return _projectType; }
            set
            {
                switch (value)
                {
                    case "website":
                        HourlyRate = 30;
                        break;
                    case "mobile":
                        HourlyRate = 40;
                        break;
                    case "misc":
                        HourlyRate = 45;
                        break;
                    default:
                        break;
                }
                _projectType = value;
            }
        }
        public string Description { get; set; }
        public int NumberOfMilestones { get; private set; }
        public virtual List<Milestone> Milestones { get; private set; }
        public int HourlyRate { get; private set; }
        public bool isActive { get; set; }
        public Guid ProjectID { get; set; }
        public void activateProject()
        {
            isActive = true;
        }
        public Project()
        {
            ProjectID = new Guid();
            isActive = false;
            NumberOfMilestones = 0;
        }
        public void addMilestone(Milestone stone)
        {
            Milestones.Add(stone);
            NumberOfMilestones += 1;
        }
        public void removeMilestone(Milestone stone)
        {
            Milestones.Remove(stone);
            NumberOfMilestones -= 1;
        }
    }
    public class Milestone
    {
        public int milestoneID { get; set; }
        public int estimatedHours { get; set; }
        public int hoursWorked { get; set; }
        public int hoursPaid { get; set; }
        public int AmountDue { get; private set; }
        public int AmountPaid { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid ParentID { get; set; }
        public virtual Project Parent { get; set; }
        public void addHours(int rate)
        {
            AmountPaid = rate * hoursPaid;
            AmountDue = (rate * hoursWorked) - AmountPaid;
        }
        public Milestone() { ParentID = Guid.Empty; }
        public Milestone(string n, string d, int estHr)
        {
            Name = n;
            Description = d;
            estimatedHours = estHr;
        }
    }
}