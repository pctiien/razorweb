using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace razorEntity.models
{
    public class AppUser : IdentityUser
    {
        [Column(TypeName="nvarchar")]
        [StringLength(100)]
        public string? HomeAddress {set;get;}
    }
}