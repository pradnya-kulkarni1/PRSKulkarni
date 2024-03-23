namespace PRSKulkarni.Models
{
    // Return only what is needed for the user login action
    public class SafeUserDTO 
    {
        // this object is created to start Login action
        public int Id { get; set; }

       
        public string Username { get; set; }

      
        public string Password { get; set; }

        public string Firstname { get; set; }

        
        public string Lastname { get; set; }

        
        public string? Phone { get; set; }
       
        public string? Email { get; set; }
        public bool Reviewer { get; set; }
        public bool Admin { get; set; }
    }
}
