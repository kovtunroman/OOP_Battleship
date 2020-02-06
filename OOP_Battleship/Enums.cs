namespace OOP_Battleship
{
    public enum OccupationType
    {
        Empty = '*',
        Battlship = 'B',
        Crusier = 'C',
        Destroyer = 'D',
        Submarine = 'S',
        Carrier = 'A',
        Miss = 'M',
        Hit = 'X'
    }

    public enum ShotResult
    {
        Miss,
        Hit
    }
}
