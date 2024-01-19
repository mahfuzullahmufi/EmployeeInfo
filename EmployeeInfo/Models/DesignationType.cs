﻿using System.ComponentModel.DataAnnotations;

namespace EmployeeInfo.Models
{
    public enum DesignationType
    {
        [Display(Name = "Associate Software Engineer")]
        AssociateSoftwareEngineer = 1,
        [Display(Name = "Software Engineer")]
        SoftwareEngineer = 2,
        [Display(Name = "Project Manager")]
        ProjectManager = 3,
    }
}
