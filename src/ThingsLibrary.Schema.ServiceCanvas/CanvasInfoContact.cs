namespace ThingsLibrary.Schema.ServiceCanvas
{ 
    /// <summary>
    /// Contact Card
    /// </summary>    
    public class CanvasInfoContactSchema
    {
        /// "Contact": 
        /// {
        ///     "Name": "Mark Lanning",
        ///     "Email": "mark.lanning@test.org",
        ///     "Company": "Symphony",
        ///     "Department": "ITO",
        ///     "ProjectUri": "https://ado.devops.com/something/_git/BaseServiceBFF"
        /// }

        /// <summary>
        /// Contact Name
        /// </summary>
        [Display(Name = "Contact Name"), StringLength(200), Required]
        public string Name { get; init; } = string.Empty;

        /// <summary>
        /// Contact Email Address
        /// </summary>
        [Display(Name = "Email"), StringLength(200), DataType(DA.DataType.EmailAddress), Required]
        public string Email { get; init; } = string.Empty;

        /// <summary>
        /// Contact Company
        /// </summary>
        [Display(Name = "Company"), StringLength(200), Required]
        public string Company { get; init; } = string.Empty;

        /// <summary>
        /// Contact Department
        /// </summary>
        [Display(Name = "Department"), StringLength(200), Required]
        public string Department { get; init; } = string.Empty;

        /// <summary>
        /// Project Url
        /// </summary>
        [Display(Name = "Project Url"), StringLength(200), DataType(DA.DataType.Url)]
        public string ProjectUrl { get; init; } = string.Empty;
    }
}
