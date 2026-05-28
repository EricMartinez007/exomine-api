namespace ExomineAPI.Models.DTOs;

public class ColonyMineralDTO
{
    public int Id { get; set; }
    public int ColonyId { get; set; }
    public int MineralId { get; set; }
    public int MineralQuantity { get; set; }

    public List<MineralDTO> Mineral { get; set; }
}