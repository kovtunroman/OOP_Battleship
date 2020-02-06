namespace OOP_Battleship.Ships
{
    public abstract class Ship
    {
        public string Name { get; set; }
        public int Length { get; set; }
        public int Hits { get; set; }
        public OccupationType OccupationType { get; set; }
        public bool IsDestroyed
        {
            get
            {
                return Hits >= Length;
            }
        }
    }
}
