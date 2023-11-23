namespace Shared.Models;

public class SegmentLocation
{
   
        public int Id { get; set; }
    
        public int Location { get; }
      
    

        public SegmentLocation( int location)
        {
            Location = location;
        }
    
}