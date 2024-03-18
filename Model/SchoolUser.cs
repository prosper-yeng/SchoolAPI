using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolAPI.Model
{
    public class SchoolUser
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string MiddleName { get; set; }
        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [Required, MaxLength(150)]
        public string Email { get; set; }
        [Required,MinLength(6), MaxLength(150)]
        public string PasswordHash { get; set; }       
        public bool AcceptTerms { get; set; }
        //public RoleEnum RoleEnum { get; set; }
        [MaxLength(1500)]
        public string VerificationToken { get; set; }
        public DateTime? Verified { get; set; }
        public bool IsVerified => Verified.HasValue || PasswordReset.HasValue;
        [MaxLength(1500)]
        public string ResetToken { get; set; }
        public DateTime? ResetTokenExpires { get; set; }
        public DateTime? PasswordReset { get; set; }       
        public long? UserLogId { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
        public long? UpdateLogId { get; set; }
        public DateTime? UpdatedOn { get; set; }
        //public List<RefreshToken> RefreshToken { get; set; }
        /*  [Required]
        public int RoleId { get; set; }
        [Required]
        public int ClientId { get; set; }
        
        public bool OwnsToken(string token)
        {
            return this.RefreshToken?.Find(x => x.Token == token) != null;
        }
        **/
    }
}