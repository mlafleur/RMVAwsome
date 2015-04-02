namespace RMV.Awesome.Interfaces
{
    public interface IBranch
    {
        string Address { get; set; }

        double Distance { get; set; }

        string Id { get; set; }

        string ImagePath { get; set; }

        double Latitude { get; set; }

        double Longitude { get; set; }

        string Title { get; set; }

        string Town { get; set; }
    }
}