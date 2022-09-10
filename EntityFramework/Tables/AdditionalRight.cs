

namespace CSharp_React.EntityFramework.Tables
{
    public class AdditionalRight
    {                      
        public int AdditionalRidhtId { get; set; }

        public int RightId { get; set; }

        public Right Right { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public bool HasRight { get; set; }
    }
}
